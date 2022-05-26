using Ezrie.IdentityService.Menus;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Blazor.Server;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Ezrie.IdentityService;

[DependsOn(
	typeof(IdentityServiceApplicationContractsModule),
	typeof(AbpAspNetCoreComponentsWebThemingModule),
	typeof(AbpAutoMapperModule)
	)]
[DependsOn(typeof(AbpIdentityBlazorServerModule))]
public class IdentityServiceBlazorModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAutoMapperObjectMapper<IdentityServiceBlazorModule>();

		Configure<AbpAutoMapperOptions>(options =>
		{
			options.AddProfile<IdentityServiceBlazorAutoMapperProfile>(validate: true);
		});

		Configure<AbpNavigationOptions>(options =>
		{
			options.MenuContributors.Add(new IdentityServiceMenuContributor());
		});

		Configure<AbpRouterOptions>(options =>
		{
			options.AdditionalAssemblies.Add(typeof(IdentityServiceBlazorModule).Assembly);
		});
	}
}
