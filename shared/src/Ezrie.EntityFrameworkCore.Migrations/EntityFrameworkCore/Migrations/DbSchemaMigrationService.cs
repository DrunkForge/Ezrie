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
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Volo.Abp.Uow;
using Volo.Abp.DependencyInjection;
using Ezrie.Migrations;

namespace Ezrie.EntityFrameworkCore.Migrations;

public class DbSchemaMigrationService : IDbSchemaMigrationService, ITransientDependency
{
	private readonly ICurrentTenant _currentTenant;
	private readonly ILogger<DbSchemaMigrationService> _logger;
	private readonly IEnumerable<IDbSchemaMigrator> _migrators;
	private readonly ITenantRepository _tenantRepository;
	private readonly IUnitOfWorkManager _unitOfWorkManager;

	public DbSchemaMigrationService(
		ICurrentTenant currentTenant,
		ILogger<DbSchemaMigrationService> logger,
		IEnumerable<IDbSchemaMigrator> migrators,
		ITenantRepository tenantRepository,
		IUnitOfWorkManager unitOfWorkManager)
	{
		_currentTenant = currentTenant;
		_logger = logger;
		_migrators = migrators;
		_tenantRepository = tenantRepository;
		_unitOfWorkManager = unitOfWorkManager;
	}

	public async Task MigrateAsync(CancellationToken cancellationToken = default)
	{
		_logger.LogInformation("Started database migrations");

		await MigrateHostAsync(cancellationToken);
		await MigrateTenantsAsync(cancellationToken);

		_logger.LogInformation("Completed database migrations");
	}

	private async Task MigrateHostAsync(CancellationToken cancellationToken)
	{
		_logger.LogInformation("Starting Host migration");

		using (var uow = _unitOfWorkManager.Begin(isTransactional: true))
		{
			try
			{
				await MigrateDatabaseSchemasAsync(null, cancellationToken);

				await uow.CompleteAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				_logger?.LogException(ex, LogLevel.Error);
				await uow.RollbackAsync(cancellationToken);
				throw;
			}
		}

		_logger.LogInformation("Completed Host migration");
	}

	private async Task MigrateTenantsAsync(CancellationToken cancellationToken)
	{
		var tenants = await GetTenantsAsync(cancellationToken);
		if (tenants == null || !tenants.Any())
		{
			_logger.LogInformation("Tenant migration will be skipped.");
			return;
		}

		_logger.LogInformation("Starting Tenant migration");

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
						_logger?.LogException(ex, LogLevel.Error);
						await uow.RollbackAsync(cancellationToken);
						throw;
					}
				}
			}

			_logger.LogInformation("Completed Tenant migration");
		}
	}

	[SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "The exception is logged and it doesn't matter why it failed.")]
	private async Task<List<Tenant>> GetTenantsAsync(CancellationToken cancellationToken)
	{
		try
		{
			return await _tenantRepository.GetListAsync(includeDetails: true, cancellationToken: cancellationToken);
		}
		catch (DependencyResolutionException)
		{
			_logger.LogWarning("Unable to resolve TenantManagementDbContext.");
		}
		catch (Exception ex)
		{
			_logger.LogWarning(ex, "Unable to retrieve Tenants.");
		}

		return new List<Tenant>();
	}

	private async Task MigrateDatabaseSchemasAsync(Tenant? tenant, CancellationToken cancellationToken)
	{
		var name = tenant == null ? "Host" : $"{tenant.Name} tenant";

		if (_migrators.Any())
		{
			_logger.LogInformation("Starting schema migration for {Name} database", name);
			foreach (var migrator in _migrators)
			{
				// TenantManagement schema should only be available in the host database (tenant == null)
				if (tenant != null && migrator.CanMigrate<ITenantManagementDbContext>())
				{
					_logger.LogDebug("Skipping {Migrator} for {Name} database as it only applies to the host database", migrator.GetType().Name, name);
					continue;
				}

				_logger.LogInformation("Applying {Migrator} to {Name} database", migrator.GetType().Name, name);
				await migrator.MigrateAsync(cancellationToken);
			}

			_logger.LogInformation("Completed schema migration for {Name} database", name);
		}
		else
		{
			_logger.LogWarning("No migrators registered for {Name} database", name);
		}
	}
}
