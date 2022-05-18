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

using Autofac.Core;
using Microsoft.Extensions.Logging;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.Uow;
using Ezrie.Migrations;

namespace Ezrie.EntityFrameworkCore.Migrations;

public abstract class DbMigrationService<T> : IDbMigrationService<T>
	where T : IDbSchemaMigrator
{
	private readonly ICurrentTenant _currentTenant;
	private readonly IEnumerable<T> _migrators;
	private readonly ITenantRepository _tenantRepository;
	private readonly IUnitOfWorkManager _unitOfWorkManager;

	protected ILogger<DbMigrationService<T>> Logger { get; set; }
	public abstract Int32 Priority { get; }

	protected DbMigrationService(
		ICurrentTenant currentTenant,
		IDataSeeder dataSeeder,
		ILogger<DbMigrationService<T>> logger,
		IEnumerable<T> migrators,
		ITenantRepository tenantRepository,
		IUnitOfWorkManager unitOfWorkManager)
	{
		_currentTenant = currentTenant;
		_migrators = migrators;
		_tenantRepository = tenantRepository;
		_unitOfWorkManager = unitOfWorkManager;
		Logger = logger;
	}

	public async Task MigrateAsync(CancellationToken cancellationToken = default)
	{
		Logger.LogInformation("Started database migrations");

		await MigrateHostAsync(cancellationToken);
		await MigrateTenantsAsync(cancellationToken);

		Logger.LogInformation("Completed database migrations");
	}

	private async Task MigrateHostAsync(CancellationToken cancellationToken)
	{
		Logger.LogInformation("Starting Host migration");

		using (var uow = _unitOfWorkManager.Begin(isTransactional: true))
		{
			try
			{
				await MigrateDatabaseSchemasAsync(null, cancellationToken);

				await uow.CompleteAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				Logger?.LogException(ex, LogLevel.Error);
				await uow.RollbackAsync(cancellationToken);
				throw;
			}
		}

		Logger.LogInformation("Completed Host migration");
	}

	private async Task MigrateTenantsAsync(CancellationToken cancellationToken)
	{
		var tenants = await GetTenantsAsync(cancellationToken);
		if (tenants == null || !tenants.Any())
		{
			Logger.LogInformation("Tenant migration will be skipped.");
			return;
		}

		Logger.LogInformation("Starting Tenant migration");

		var migratedDatabaseSchemas = new HashSet<String>();

		foreach (var tenant in tenants)
		{
			using (_currentTenant.Change(tenant.Id))
			{
				using (var uow = _unitOfWorkManager.Begin(isTransactional: true))
				{
					try
					{
						var tenantConnectionStrings = tenant.ConnectionStrings
							.Select(x => x.Value)
							.ToList();

						if (!migratedDatabaseSchemas.IsSupersetOf(tenantConnectionStrings))
						{
							await MigrateDatabaseSchemasAsync(tenant, cancellationToken);

							migratedDatabaseSchemas.AddIfNotContains(tenantConnectionStrings);
						}

						await uow.CompleteAsync(cancellationToken);
					}
					catch (Exception ex)
					{
						Logger?.LogException(ex, LogLevel.Error);
						await uow.RollbackAsync(cancellationToken);
						throw;
					}
				}
			}

			Logger.LogInformation("Completed Tenant migration");
		}
	}

	private async Task<List<Tenant>> GetTenantsAsync(CancellationToken cancellationToken)
	{
		try
		{
			return await _tenantRepository.GetListAsync(includeDetails: true, cancellationToken: cancellationToken);
		}
		catch (DependencyResolutionException)
		{
			Logger.LogWarning("Unable to resolve TenantManagementDbContext.");
			return new List<Tenant>();
		}
	}

	private async Task MigrateDatabaseSchemasAsync(Tenant? tenant, CancellationToken cancellationToken)
	{
		var name = tenant == null ? "Host" : $"{tenant.Name} tenant";

		if (_migrators.Any())
		{
			Logger.LogInformation("Starting schema migration for {Name} database", name);
			foreach (var migrator in _migrators)
			{
				// TenantManagement schema should only be available in the host database (tenant == null)
				if (tenant != null && migrator.CanMigrate<ITenantManagementDbContext>())
				{
					Logger.LogDebug("Skipping {Migrator} for {Name} database as it only applies to the host database", migrator.GetType().Name, name);
					continue;
				}

				Logger.LogInformation("Applying {Migrator} to {Name} database", migrator.GetType().Name, name);
				await migrator.MigrateAsync(cancellationToken);
			}

			Logger.LogInformation("Completed schema migration for {Name} database", name);
		}
		else
		{
			Logger.LogWarning("No migrators registered for {Name} database", name);
		}
	}
}
