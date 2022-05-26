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
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using Volo.Abp.Uow;

namespace Ezrie.EntityFrameworkCore.Seeding;

public class SeedingService : ITransientDependency
{
	private readonly ICurrentTenant _currentTenant;
	private readonly IDataSeeder _dataSeeder;
	private readonly ITenantRepository _tenantRepository;
	private readonly IUnitOfWorkManager _unitOfWorkManager;

	protected ILogger<SeedingService> Logger { get; set; }

	public SeedingService(
		ICurrentTenant currentTenant,
		IDataSeeder dataSeeder,
		ILogger<SeedingService> logger,
		ITenantRepository tenantRepository,
		IUnitOfWorkManager unitOfWorkManager)
	{
		_currentTenant = currentTenant;
		_dataSeeder = dataSeeder;
		_tenantRepository = tenantRepository;
		_unitOfWorkManager = unitOfWorkManager;
		Logger = logger;
	}

	public async Task SeedAsync(CancellationToken cancellationToken = default)
	{
		Logger.LogInformation("Started seeding databases");

		await SeedHostAsync(cancellationToken);
		await SeedTenantsAsync(cancellationToken);

		Logger.LogInformation("Completed seeding databases");
	}

	private async Task SeedHostAsync(CancellationToken cancellationToken)
	{
		Logger.LogInformation("Starting Host seeding");

		await SeedDataAsync(null, cancellationToken);

		Logger.LogInformation("Completed Host seeding");
	}

	private async Task SeedTenantsAsync(CancellationToken cancellationToken)
	{
		var tenants = await GetTenantsAsync(cancellationToken);
		if (tenants == null || !tenants.Any())
		{
			Logger.LogWarning("No tenants found. Tenant seeding will be skipped.");
			return;
		}

		Logger.LogInformation("Starting Tenant seeding");

		foreach (var tenant in tenants)
		{
			await SeedDataAsync(tenant, cancellationToken);

		}

		Logger.LogInformation("Completed Tenant seeding");
	}

	private async Task SeedDataAsync(Tenant? tenant, CancellationToken cancellationToken)
	{
		using (_currentTenant.Change(tenant?.Id))
		{
			using (var uow = _unitOfWorkManager.Begin(isTransactional: true))
			{
				try
				{
					var name = tenant == null ? "HOST" : $"{tenant.Name} ({tenant.Id})";
					Logger.LogInformation("Started Seeding {Name}", name);

					await _dataSeeder.SeedAsync(tenant?.Id);

					Logger.LogInformation("Completed Seeding {Name}", name);

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
			Logger.LogWarning("Unable to resolve TenantManagementDbContext.");
		}
		catch (Exception ex)
		{
			Logger.LogWarning(ex, "Unable to retrieve Tenants.");
		}

		return new List<Tenant>();
	}
}
