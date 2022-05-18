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

using Ezrie.AdministrationService.Data;
using Ezrie.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using Volo.Abp.Uow;

namespace Ezrie.AdministrationService.EntityFrameworkCore;

public class AdministrationServiceDbMigrationService : DbMigrationService<IAdministrationServiceDbSchemaMigrator>, IAdministrationServiceDbMigrationService, ITransientDependency
{
	public AdministrationServiceDbMigrationService(
		ICurrentTenant currentTenant,
		IDataSeeder dataSeeder,
		ILogger<DbMigrationService<IAdministrationServiceDbSchemaMigrator>> logger,
		IEnumerable<IAdministrationServiceDbSchemaMigrator> migrators,
		ITenantRepository tenantRepository,
		IUnitOfWorkManager unitOfWorkManager
	) : base(currentTenant, dataSeeder, logger, migrators, tenantRepository, unitOfWorkManager)
	{
	}

	public override Int32 Priority => 0;
}
