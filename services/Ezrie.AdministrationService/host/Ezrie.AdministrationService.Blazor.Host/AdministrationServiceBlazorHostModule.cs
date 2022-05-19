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

using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Ezrie.AdministrationService.Blazor.WebAssembly;
using Ezrie.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Components.Web.BasicTheme.Themes.Basic;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Components.WebAssembly.BasicTheme;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Blazor.WebAssembly;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Blazor.WebAssembly;
using Volo.Abp.TenantManagement.Blazor.WebAssembly;
using Volo.Abp.UI.Navigation;

namespace Ezrie.AdministrationService.Blazor.Host;

[DependsOn(typeof(AbpAutofacWebAssemblyModule))]
[DependsOn(typeof(AbpAspNetCoreComponentsWebAssemblyBasicThemeModule))]
[DependsOn(typeof(AbpAccountApplicationContractsModule))]
[DependsOn(typeof(AbpIdentityBlazorWebAssemblyModule))]
[DependsOn(typeof(AbpTenantManagementBlazorWebAssemblyModule))]
[DependsOn(typeof(AbpSettingManagementBlazorWebAssemblyModule))]
[DependsOn(typeof(AdministrationServiceBlazorWebAssemblyModule))]
public class AdministrationServiceBlazorHostModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
		var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

		ConfigureAuthentication(builder);
		ConfigureHttpClient(context, environment);
		ConfigureBlazorise(context);
		ConfigureRouter(context);
		ConfigureUI(builder);
		ConfigureMenu(context);
		ConfigureAutoMapper(context);

		//context.Services.ConfigureCors();
	}

	private void ConfigureRouter(ServiceConfigurationContext context)
	{
		Configure<AbpRouterOptions>(options =>
		{
			options.AppAssembly = typeof(AdministrationServiceBlazorHostModule).Assembly;
		});
	}

	private void ConfigureMenu(ServiceConfigurationContext context)
	{
		Configure<AbpNavigationOptions>(options =>
		{
			options.MenuContributors.Add(new AdministrationServiceHostMenuContributor(context.Services.GetConfiguration()));
		});
	}

	private static void ConfigureBlazorise(ServiceConfigurationContext context)
	{
		context.Services
			.AddBootstrap5Providers()
			.AddFontAwesomeIcons();
	}

	private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
	{
		builder.Services.AddOidcAuthentication(options =>
		{
			var apiConfiguration = builder.Configuration.GetApiConfiguration();
			options.ProviderOptions.Authority = apiConfiguration.IdentityServerBaseUrl;
			options.ProviderOptions.ClientId = apiConfiguration.ClientId;
			options.ProviderOptions.DefaultScopes.Add(apiConfiguration.OidcApiName);
			options.ProviderOptions.ResponseType = "code";
		});
	}

	private static void ConfigureUI(WebAssemblyHostBuilder builder)
	{
		builder.RootComponents.Add<App>("#ApplicationContainer");
	}

	private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
	{
		context.Services.AddTransient(sp => new HttpClient
		{
			BaseAddress = new Uri(environment.BaseAddress)
		});
	}

	private void ConfigureAutoMapper(ServiceConfigurationContext context)
	{
		Configure<AbpAutoMapperOptions>(options =>
		{
			options.AddMaps<AdministrationServiceBlazorHostModule>();
		});
	}
}
