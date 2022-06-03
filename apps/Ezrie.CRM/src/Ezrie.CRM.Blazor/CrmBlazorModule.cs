using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Ezrie.AdministrationService;
using Ezrie.Configuration;
using Ezrie.CRM.Menus;
using Ezrie.CRM.Services;
using Ezrie.IdentityService;
using Ezrie.TenantService;
using IdentityModel;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Syncfusion.Blazor;
using Syncfusion.Licensing;
using Volo.Abp.AspNetCore.Components.Web.BasicTheme.Themes.Basic;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Components.WebAssembly.BasicTheme;
using Volo.Abp.Autofac.WebAssembly;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Ezrie.CRM;

[DependsOn(typeof(CrmHttpApiClientModule))]
[DependsOn(typeof(AdministrationServiceBlazorWebAssemblyModule))]
[DependsOn(typeof(IdentityServiceBlazorWebAssemblyModule))]
[DependsOn(typeof(TenantServiceBlazorWebAssemblyModule))]

[DependsOn(typeof(AbpAspNetCoreComponentsWebAssemblyBasicThemeModule))]
[DependsOn(typeof(AbpAutofacWebAssemblyModule))]
public class CrmBlazorModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		var environment = context.Services.GetSingletonInstance<IWebAssemblyHostEnvironment>();
		var builder = context.Services.GetSingletonInstance<WebAssemblyHostBuilder>();

		ConfigureAuthentication(builder);
		ConfigureAuthorization(builder);
		ConfigureAutoMapper(context);
		ConfigureBlazorise(context);
		ConfigureHttpClient(context, environment);
		ConfigureMenu(context);
		ConfigureRouter(context);
		ConfigureUI(builder);
	}

	private static void ConfigureAuthentication(WebAssemblyHostBuilder builder)
	{
		builder.Services.AddOidcAuthentication(options =>
		{
			builder.Configuration.Bind("AuthServer", options.ProviderOptions);
			options.UserOptions.RoleClaim = JwtClaimTypes.Role;
			options.ProviderOptions.DefaultScopes.Add("email");
			options.ProviderOptions.DefaultScopes.Add("phone");
			options.ProviderOptions.DefaultScopes.Add("role");
		});
	}

	private static void ConfigureAuthorization(WebAssemblyHostBuilder builder)
	{
		builder.Services.AddAuthorizationCore();
		builder.Services.TryAddSingleton<AuthenticationStateProvider, HostAuthenticationStateProvider>();
		builder.Services.TryAddSingleton(sp => (HostAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());
		builder.Services.AddTransient<AuthorizedHandler>();
	}

	private static void ConfigureAuthorizedHttpClient(WebAssemblyHostBuilder builder)
	{
		var remoteServices = builder.Configuration.GetRemoteServices();

		builder.Services
			.AddTransient(sp => new HttpClient()
			{
				BaseAddress = new Uri(remoteServices.Default.BaseUrl)
			})
			.AddHttpClient("authorizedClient", client => client.BaseAddress = new Uri(remoteServices.Default.BaseUrl))
			.AddHttpMessageHandler<AuthorizedHandler>();
	}

	private void ConfigureAutoMapper(ServiceConfigurationContext context)
	{
		Configure<AbpAutoMapperOptions>(options =>
		{
			options.AddMaps<CrmBlazorModule>();
		});
	}

	private static void ConfigureBlazorise(ServiceConfigurationContext context)
	{
		context.Services
			.AddBootstrap5Providers()
			.AddFontAwesomeIcons();
	}

	private static void ConfigureHttpClient(ServiceConfigurationContext context, IWebAssemblyHostEnvironment environment)
	{
		context.Services.AddTransient(sp => new HttpClient
		{
			BaseAddress = new Uri(environment.BaseAddress)
		});
	}

	private void ConfigureMenu(ServiceConfigurationContext context)
	{
		Configure<AbpNavigationOptions>(options =>
		{
			options.MenuContributors.Add(new CrmMenuContributor(context.Services.GetConfiguration()));
		});
	}

	private void ConfigureRouter(ServiceConfigurationContext context)
	{
		Configure<AbpRouterOptions>(options =>
		{
			options.AppAssembly = typeof(CrmBlazorModule).Assembly;
		});
	}

	private static void ConfigureUI(WebAssemblyHostBuilder builder)
	{
		builder.RootComponents.Add<App>("#ApplicationContainer");
		builder.RootComponents.Add<HeadOutlet>("head::after");
		builder.Services.AddSyncfusionBlazor();
		SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHFqVVhkW1pFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF9jTH5adEViX3pWd3VWQQ==;Mgo+DSMBPh8sVXJ0S0d+XE9AcVRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS3xTc0VqW39fdnFdQWJbVg==;Mgo+DSMBMAY9C3t2VVhhQlFaclhJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxRdkBjUH1ecXJQTmdfWUM=;NjMzODU4QDMyMzAyZTMxMmUzMGJkTTI0bDdBcHRva3E1SDl4K2F6SFBjWmdyYk1uaVBhZGNCMlhWZXdtVDg9");
	}
}
