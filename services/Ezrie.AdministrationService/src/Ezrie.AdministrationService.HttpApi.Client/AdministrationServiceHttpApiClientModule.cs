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

using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.VirtualFileSystem;

namespace Ezrie.AdministrationService;

[DependsOn(typeof(AdministrationServiceApplicationContractsModule))]
[DependsOn(typeof(AbpHttpClientModule))]
[DependsOn(typeof(AbpFeatureManagementHttpApiClientModule))]
[DependsOn(typeof(AbpPermissionManagementHttpApiClientModule))]
[DependsOn(typeof(AbpSettingManagementHttpApiClientModule))]
public class AdministrationServiceHttpApiClientModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddHttpClientProxies(
			typeof(AdministrationServiceApplicationContractsModule).Assembly,
			AdministrationServiceRemoteServiceConsts.RemoteServiceName
		);

		Configure<AbpVirtualFileSystemOptions>(options =>
		{
			options.FileSets.AddEmbedded<AdministrationServiceHttpApiClientModule>();
		});

	}
}
