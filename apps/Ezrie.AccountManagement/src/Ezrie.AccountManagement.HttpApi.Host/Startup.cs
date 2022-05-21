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

using System.IdentityModel.Tokens.Jwt;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Skoruba.AuditLogging.EntityFramework.Entities;
using Skoruba.IdentityServer4.Shared.Configuration.Helpers;
using Serilog;
using Ezrie.AccountManagement.Identity;
using Ezrie.AccountManagement.Resources;
using Ezrie.AccountManagement.Mappers;
using Ezrie.AccountManagement.ExceptionHandling;
using Ezrie.AccountManagement.Helpers;
using Ezrie.AccountManagement.Configuration.Authorization;
using Ezrie.Configuration;
using Ezrie.Hosting.AspNetCore;
using Ezrie.AccountManagement.EntityFrameworkCore;

namespace Ezrie.AccountManagement;

public class Startup
{
	public Startup(IWebHostEnvironment env, IConfiguration configuration)
	{
		JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
		HostingEnvironment = env;
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	public IWebHostEnvironment HostingEnvironment { get; }

	public void ConfigureServices(IServiceCollection services)
	{
		var apiConfiguration = Configuration.GetAppConfiguration();
		services.AddSingleton(apiConfiguration);

		// Add DbContexts
		RegisterDbContexts(services);

		services.AddDataProtection<IdentityServerDataProtectionDbContext>(Configuration);

		// Add email senders which is currently setup for SendGrid and SMTP
		services.AddEmailSenders(Configuration);

		services.AddScoped<ControllerExceptionFilterAttribute>();
		services.AddScoped<IApiErrorResources, ApiErrorResources>();

		// Add authentication services
		RegisterAuthentication(services);

		// Add authorization services
		RegisterAuthorization(services);

		var profileTypes = new HashSet<Type>
			{
				typeof(IdentityMapperProfile<IdentityRoleDto, IdentityUserRolesDto, String, IdentityUserClaimsDto, IdentityUserClaimDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto, IdentityRoleClaimDto, IdentityRoleClaimsDto>)
			};

		services.AddAdminAspNetIdentityServices<AdminIdentityDbContext, IdentityServerPersistedGrantDbContext,
			IdentityUserDto, IdentityRoleDto, UserIdentity, UserIdentityRole, String, UserIdentityUserClaim, UserIdentityUserRole,
			UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken,
			IdentityUsersDto, IdentityRolesDto, IdentityUserRolesDto,
			IdentityUserClaimsDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto,
			IdentityRoleClaimsDto, IdentityUserClaimDto, IdentityRoleClaimDto>(profileTypes);

		services.AddAdminServices<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext>();

		services.AddMvcServices<IdentityUserDto, IdentityRoleDto,
			UserIdentity, UserIdentityRole, String, UserIdentityUserClaim, UserIdentityUserRole,
			UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken,
			IdentityUsersDto, IdentityRolesDto, IdentityUserRolesDto,
			IdentityUserClaimsDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto,
			IdentityRoleClaimsDto, IdentityUserClaimDto, IdentityRoleClaimDto>();

		services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc(apiConfiguration.ApiVersion, new OpenApiInfo { Title = apiConfiguration.ApiName, Version = apiConfiguration.ApiVersion });

			options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
			{
				Type = SecuritySchemeType.OAuth2,
				Flows = new OpenApiOAuthFlows
				{
					AuthorizationCode = new OpenApiOAuthFlow
					{
						AuthorizationUrl = new Uri($"{apiConfiguration.IdentityServerBaseUrl}/connect/authorize"),
						TokenUrl = new Uri($"{apiConfiguration.IdentityServerBaseUrl}/connect/token"),
						Scopes = new Dictionary<String, String> { { apiConfiguration.OidcApiName, apiConfiguration.ApiName } }
					}
				}
			});

			options.OperationFilter<AuthorizeCheckOperationFilter>();

			options.HideAbpEndpoints();
		});

		services.AddAuditEventLogging<AdminAuditLogDbContext, AuditLog>(Configuration);

		services.AddIdSHealthChecks<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminIdentityDbContext, AdminLogDbContext, AdminAuditLogDbContext, IdentityServerDataProtectionDbContext>(Configuration, apiConfiguration);

		services.ConfigureCors();
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		var apiConfiguration = app.ApplicationServices.GetAppConfiguration();

		app.UseSerilogRequestLogging();
		app.AddForwardHeaders();

		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
		}

		app.UseSwagger();
		app.UseSwaggerUI(c =>
		{
			c.SwaggerEndpoint($"{apiConfiguration.ApiBaseUrl}/swagger/v1/swagger.json", apiConfiguration.ApiName);

			c.OAuthClientId(apiConfiguration.ClientId);
			c.OAuthAppName(apiConfiguration.ApiName);
			c.OAuthUsePkce();
		});

		app.UseRouting();
		app.UseCors();
		UseAuthentication(app);
		app.UseAuthorization();
		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
			endpoints.MapDefaultControllerRoute();

			endpoints.MapHealthChecks("/health", new HealthCheckOptions
			{
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});
		});
	}

	public virtual void RegisterDbContexts(IServiceCollection services)
	{
		services.AddDbContexts<AdminIdentityDbContext, IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext, IdentityServerDataProtectionDbContext, AuditLog>(Configuration);
	}

	public virtual void RegisterAuthentication(IServiceCollection services)
	{
		services.AddApiAuthentication<AdminIdentityDbContext, UserIdentity, UserIdentityRole>(Configuration);
	}

	public virtual void RegisterAuthorization(IServiceCollection services)
	{
		services.AddAuthorizationPolicies();
	}

	// This gets overriden by unit tests.
	public virtual void UseAuthentication(IApplicationBuilder app)
	{
		app.UseAuthentication();
	}
}

