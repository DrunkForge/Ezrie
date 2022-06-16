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

using Ezrie.Logging;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Ezrie.EntityFrameworkCore.Migrations;

[DependsOn(typeof(EzrieEntityFrameworkCoreModule))]
[DependsOn(typeof(EzrieLoggingModule))]
[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpMultiTenancyModule))]
[DependsOn(typeof(AbpDataModule))]
// We need to loop through tenants to migrate and seed their databases
[DependsOn(typeof(AbpTenantManagementApplicationContractsModule))]
[DependsOn(typeof(AbpTenantManagementEntityFrameworkCoreModule))]
public class EzrieEntityFrameworkCoreMigrationsModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
	}
}
