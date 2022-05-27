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

using Ezrie.AdministrationService.Localization;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Ezrie.AdministrationService;

[DependsOn(typeof(AbpValidationModule))]
[DependsOn(typeof(AbpFeatureManagementDomainSharedModule))]
[DependsOn(typeof(AbpPermissionManagementDomainSharedModule))]
[DependsOn(typeof(AbpSettingManagementDomainSharedModule))]
public class AdministrationServiceDomainSharedModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpVirtualFileSystemOptions>(options =>
		{
			options.FileSets.AddEmbedded<AdministrationServiceDomainSharedModule>();
		});

		Configure<AbpLocalizationOptions>(options =>
		{
			options.Resources
				.Add<AdministrationServiceResource>("en")
				.AddBaseTypes(typeof(AbpValidationResource))
				.AddVirtualJson("/Localization/AdministrationService");
		});

		Configure<AbpExceptionLocalizationOptions>(options =>
		{
			options.MapCodeNamespace("AdministrationService", typeof(AdministrationServiceResource));
		});
	}
}
