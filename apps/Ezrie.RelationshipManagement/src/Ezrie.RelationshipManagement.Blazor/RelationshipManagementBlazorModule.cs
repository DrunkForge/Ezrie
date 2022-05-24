using System;
using System.Net.Http;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using IdentityModel;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ezrie.RelationshipManagement.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.BasicTheme.Themes.Basic;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.AspNetCore.Components.WebAssembly.BasicTheme;
using Volo.Abp.Identity.Blazor.WebAssembly;
using Volo.Abp.SettingManagement.Blazor.WebAssembly;
using Volo.Abp.TenantManagement.Blazor.WebAssembly;
using Volo.Abp.FeatureManagement.Blazor.WebAssembly;
using Volo.Abp.PermissionManagement.Blazor.WebAssembly;

namespace Ezrie.RelationshipManagement.Blazor;

[DependsOn(typeof(RelationshipManagementHttpApiClientModule))]
[DependsOn(typeof(AbpAspNetCoreComponentsWebAssemblyBasicThemeModule))]
[DependsOn(typeof(AbpAutofacWebAssemblyModule))]
[DependsOn(typeof(AbpIdentityBlazorWebAssemblyModule))]
[DependsOn(typeof(AbpFeatureManagementBlazorWebAssemblyModule))]
[DependsOn(typeof(AbpIdentityBlazorWebAssemblyModule))]
[DependsOn(typeof(AbpPermissionManagementBlazorWebAssemblyModule))]
[DependsOn(typeof(AbpSettingManagementBlazorWebAssemblyModule))]
[DependsOn(typeof(AbpTenantManagementBlazorWebAssemblyModule))]
public class RelationshipManagementBlazorModule : AbpModule
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
	}

	private void ConfigureRouter(ServiceConfigurationContext context)
	{
		Configure<AbpRouterOptions>(options =>
		{
			options.AppAssembly = typeof(RelationshipManagementBlazorModule).Assembly;
		});
	}

	private void ConfigureMenu(ServiceConfigurationContext context)
	{
		Configure<AbpNavigationOptions>(options =>
		{
			options.MenuContributors.Add(new RelationshipManagementMenuContributor(context.Services.GetConfiguration()));
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
			builder.Configuration.Bind("AuthServer", options.ProviderOptions);
			options.UserOptions.RoleClaim = JwtClaimTypes.Role;
			options.ProviderOptions.DefaultScopes.Add("ezrie_relationship_management_api");
			options.ProviderOptions.DefaultScopes.Add("role");
			options.ProviderOptions.DefaultScopes.Add("email");
			options.ProviderOptions.DefaultScopes.Add("phone");
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
			options.AddMaps<RelationshipManagementBlazorModule>();
		});
	}
}
