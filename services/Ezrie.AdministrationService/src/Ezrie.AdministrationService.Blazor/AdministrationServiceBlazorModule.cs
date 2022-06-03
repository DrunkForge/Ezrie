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

using Ezrie.AdministrationService.Menus;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement.Blazor;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Blazor;
using Volo.Abp.SettingManagement.Blazor;
using Volo.Abp.UI.Navigation;

namespace Ezrie.AdministrationService;

[DependsOn(typeof(AdministrationServiceApplicationContractsModule))]

[DependsOn(typeof(AbpAspNetCoreComponentsWebThemingModule))]
[DependsOn(typeof(AbpAutoMapperModule))]
[DependsOn(typeof(AbpFeatureManagementBlazorModule))]
[DependsOn(typeof(AbpPermissionManagementBlazorModule))]
[DependsOn(typeof(AbpSettingManagementBlazorModule))]
public class AdministrationServiceBlazorModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		ConfigureAutoMapper(context);

		ConfigureNavigation();

		ConfigureRouter();
	}

	private void ConfigureAutoMapper(ServiceConfigurationContext context)
	{
		context.Services.AddAutoMapperObjectMapper<AdministrationServiceBlazorModule>();

		Configure<AbpAutoMapperOptions>(options =>
		{
			options.AddProfile<AdministrationServiceBlazorAutoMapperProfile>(validate: true);
		});
	}

	private void ConfigureNavigation() => Configure<AbpNavigationOptions>(options =>
	{
		options.MenuContributors.Add(new AdministrationServiceMenuContributor());
	});

	private void ConfigureRouter() => Configure<AbpRouterOptions>(options =>
	{
		options.AdditionalAssemblies.Add(typeof(AdministrationServiceBlazorModule).Assembly);
	});
}
