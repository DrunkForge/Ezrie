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

using global::Localization.Resources.AbpUi;
using Ezrie.AdministrationService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Ezrie.AdministrationService;

[DependsOn(typeof(AdministrationServiceApplicationContractsModule))]
[DependsOn(typeof(AbpAspNetCoreMvcModule))]
public class AdministrationServiceHttpApiModule : AbpModule
{
	public override void PreConfigureServices(ServiceConfigurationContext context)
	{
		PreConfigure<IMvcBuilder>(mvcBuilder =>
		{
			mvcBuilder.AddApplicationPartIfNotExists(typeof(AdministrationServiceHttpApiModule).Assembly);
		});
	}

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpLocalizationOptions>(options =>
		{
			options.Resources
				.Get<AdministrationServiceResource>()
				.AddBaseTypes(typeof(AbpUiResource));
		});
	}
}
