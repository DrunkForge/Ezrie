/*********************************************************************************************
* EzrieCRM
* Copyright (C) 2022 Doug Wilson (info@dougwilson.ca)
* 
* This program is free software: you can redistribute it and/or modify it under the terms of
* the GNU Affero General Public License as published by the Free Software Foundation, either
* version 3 of the License, or (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
* without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
* See the GNU Affero General Public License for more details.
* 
* You should have received a copy of the GNU Affero General Public License along with this
* program. If not, see <https://www.gnu.org/licenses/>.
*********************************************************************************************/

using IdentityModel;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skoruba.AuditLogging.EntityFramework.DbContexts;
using Skoruba.AuditLogging.EntityFramework.Entities;
using Skoruba.IdentityServer4.Admin.EntityFramework.Configuration.Configuration;
using Skoruba.IdentityServer4.Admin.EntityFramework.Interfaces;

namespace Ezrie.AccountManagement.EntityFrameworkCore.EntityFrameworkCore;

public static class DbMigrationHelpers
{
	/// <summary>
	/// Generate migrations before running this method, you can use these steps bellow:
	/// https://github.com/skoruba/IdentityServer4.Admin#ef-core--data-access
	/// </summary>
	/// <param name="host"></param>
	/// <param name="applyDbMigrationWithDataSeedFromProgramArguments"></param>
	/// <param name="seedConfiguration"></param>
	/// <param name="databaseMigrationsConfiguration"></param>
	public static async Task<Boolean> ApplyDbMigrationsWithDataSeedAsync<TIdentityServerDbContext, TIdentityDbContext,
		TPersistedGrantDbContext, TLogDbContext, TAuditLogDbContext, TDataProtectionDbContext, TUser, TRole>(
		IHost host, Boolean applyDbMigrationWithDataSeedFromProgramArguments, SeedConfiguration seedConfiguration,
		DatabaseMigrationsConfiguration databaseMigrationsConfiguration)
		where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
		where TIdentityDbContext : DbContext
		where TPersistedGrantDbContext : DbContext, IAdminPersistedGrantDbContext
		where TLogDbContext : DbContext, IAdminLogDbContext
		where TAuditLogDbContext : DbContext, IAuditLoggingDbContext<AuditLog>
		where TDataProtectionDbContext : DbContext, IDataProtectionKeyContext
		where TUser : IdentityUser, new()
		where TRole : IdentityRole, new()
	{
		var migrationComplete = false;
		using (var serviceScope = host.Services.CreateScope())
		{
			var services = serviceScope.ServiceProvider;

			if (databaseMigrationsConfiguration != null && databaseMigrationsConfiguration.ApplyDatabaseMigrations
				|| applyDbMigrationWithDataSeedFromProgramArguments)
			{
				migrationComplete = await EnsureDatabasesMigratedAsync<TIdentityDbContext, TIdentityServerDbContext, TPersistedGrantDbContext, TLogDbContext, TAuditLogDbContext, TDataProtectionDbContext>(services);
			}

			if (seedConfiguration != null && seedConfiguration.ApplySeed
				|| applyDbMigrationWithDataSeedFromProgramArguments)
			{
				var seedComplete = await EnsureSeedDataAsync<TIdentityServerDbContext, TUser, TRole>(services);
				return migrationComplete && seedComplete;
			}

		}

		return migrationComplete;
	}

	public static async Task<Boolean> EnsureDatabasesMigratedAsync<TIdentityDbContext, TConfigurationDbContext, TPersistedGrantDbContext, TLogDbContext, TAuditLogDbContext, TDataProtectionDbContext>(IServiceProvider services)
		where TIdentityDbContext : DbContext
		where TPersistedGrantDbContext : DbContext
		where TConfigurationDbContext : DbContext
		where TLogDbContext : DbContext
		where TAuditLogDbContext : DbContext
		where TDataProtectionDbContext : DbContext
	{
		var pendingMigrationCount = 0;
		using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
		{
			using (var context = scope.ServiceProvider.GetRequiredService<TPersistedGrantDbContext>())
			{
				await context.Database.MigrateAsync();
				pendingMigrationCount += (await context.Database.GetPendingMigrationsAsync()).Count();
			}

			using (var context = scope.ServiceProvider.GetRequiredService<TIdentityDbContext>())
			{
				await context.Database.MigrateAsync();
				pendingMigrationCount += (await context.Database.GetPendingMigrationsAsync()).Count();
			}

			using (var context = scope.ServiceProvider.GetRequiredService<TConfigurationDbContext>())
			{
				await context.Database.MigrateAsync();
				pendingMigrationCount += (await context.Database.GetPendingMigrationsAsync()).Count();
			}

			using (var context = scope.ServiceProvider.GetRequiredService<TLogDbContext>())
			{
				await context.Database.MigrateAsync();
				pendingMigrationCount += (await context.Database.GetPendingMigrationsAsync()).Count();
			}

			using (var context = scope.ServiceProvider.GetRequiredService<TAuditLogDbContext>())
			{
				await context.Database.MigrateAsync();
				pendingMigrationCount += (await context.Database.GetPendingMigrationsAsync()).Count();
			}

			using (var context = scope.ServiceProvider.GetRequiredService<TDataProtectionDbContext>())
			{
				await context.Database.MigrateAsync();
				pendingMigrationCount += (await context.Database.GetPendingMigrationsAsync()).Count();
			}
		}

		return pendingMigrationCount == 0;
	}

	public static async Task<Boolean> EnsureSeedDataAsync<TIdentityServerDbContext, TUser, TRole>(IServiceProvider serviceProvider)
	where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
	where TUser : IdentityUser, new()
	where TRole : IdentityRole, new()
	{
		using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
		{
			var context = scope.ServiceProvider.GetRequiredService<TIdentityServerDbContext>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<TUser>>();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<TRole>>();
			var idsDataConfiguration = scope.ServiceProvider.GetRequiredService<IdentityServerData>();
			var idDataConfiguration = scope.ServiceProvider.GetRequiredService<IdentityData>();

			await EnsureSeedIdentityServerData(context, idsDataConfiguration);
			await EnsureSeedIdentityData(userManager, roleManager, idDataConfiguration);
			return true;
		}
	}

	/// <summary>
	/// Generate default admin user / role
	/// </summary>
	private static async Task EnsureSeedIdentityData<TUser, TRole>(UserManager<TUser> userManager,
		RoleManager<TRole> roleManager, IdentityData identityDataConfiguration)
		where TUser : IdentityUser, new()
		where TRole : IdentityRole, new()
	{
		// adding roles from seed
		foreach (var r in identityDataConfiguration.Roles)
		{
			if (!await roleManager.RoleExistsAsync(r.Name))
			{
				var role = new TRole
				{
					Name = r.Name
				};

				var result = await roleManager.CreateAsync(role);

				if (result.Succeeded)
				{
					foreach (var claim in r.Claims)
					{
						await roleManager.AddClaimAsync(role, new System.Security.Claims.Claim(claim.Type, claim.Value));
					}
				}
			}
		}

		// adding users from seed
		foreach (var user in identityDataConfiguration.Users)
		{
			var identityUser = new TUser
			{
				UserName = user.Username,
				Email = user.Email,
				EmailConfirmed = true
			};

			var userByUserName = await userManager.FindByNameAsync(user.Username);
			var userByEmail = await userManager.FindByEmailAsync(user.Email);

			// User is already exists in database
			if (userByUserName != default || userByEmail != default)
			{
				continue;
			}

			// if there is no password we create user without password
			// user can reset password later, because accounts have EmailConfirmed set to true
			var result = !String.IsNullOrEmpty(user.Password)
			? await userManager.CreateAsync(identityUser, user.Password)
			: await userManager.CreateAsync(identityUser);

			if (result.Succeeded)
			{
				foreach (var claim in user.Claims)
				{
					await userManager.AddClaimAsync(identityUser, new System.Security.Claims.Claim(claim.Type, claim.Value));
				}

				foreach (var role in user.Roles)
				{
					await userManager.AddToRoleAsync(identityUser, role);
				}
			}
		}
	}

	/// <summary>
	/// Generate default clients, identity and api resources
	/// </summary>
	private static async Task EnsureSeedIdentityServerData<TIdentityServerDbContext>(TIdentityServerDbContext context, IdentityServerData identityServerDataConfiguration)
		where TIdentityServerDbContext : DbContext, IAdminConfigurationDbContext
	{
		foreach (var resource in identityServerDataConfiguration.IdentityResources)
		{
			var exits = await context.IdentityResources.AnyAsync(a => a.Name == resource.Name);

			if (exits)
			{
				continue;
			}

			await context.IdentityResources.AddAsync(resource.ToEntity());
		}

		foreach (var apiScope in identityServerDataConfiguration.ApiScopes)
		{
			var exits = await context.ApiScopes.AnyAsync(a => a.Name == apiScope.Name);

			if (exits)
			{
				continue;
			}

			await context.ApiScopes.AddAsync(apiScope.ToEntity());
		}

		foreach (var resource in identityServerDataConfiguration.ApiResources)
		{
			var exits = await context.ApiResources.AnyAsync(a => a.Name == resource.Name);

			if (exits)
			{
				continue;
			}

			foreach (var s in resource.ApiSecrets)
			{
				s.Value = s.Value.ToSha256();
			}

			await context.ApiResources.AddAsync(resource.ToEntity());
		}

		foreach (var client in identityServerDataConfiguration.Clients)
		{
			var exits = await context.Clients.AnyAsync(a => a.ClientId == client.ClientId);

			if (exits)
			{
				continue;
			}

			foreach (var secret in client.ClientSecrets)
			{
				secret.Value = secret.Value.ToSha256();
			}

			client.Claims = client.ClientClaims
				.Select(c => new ClientClaim(c.Type, c.Value))
				.ToList();

			await context.Clients.AddAsync(client.ToEntity());
		}

		await context.SaveChangesAsync();
	}
}

