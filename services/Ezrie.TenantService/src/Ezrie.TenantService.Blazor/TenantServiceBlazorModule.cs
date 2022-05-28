using Ezrie.TenantService.Menus;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Ezrie.TenantService;

[DependsOn(typeof(TenantServiceApplicationContractsModule))]
[DependsOn(typeof(AbpAspNetCoreComponentsWebThemingModule))]
[DependsOn(typeof(AbpAutoMapperModule))]
public class TenantServiceBlazorModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAutoMapperObjectMapper<TenantServiceBlazorModule>();

		Configure<AbpAutoMapperOptions>(options =>
		{
			options.AddProfile<TenantServiceBlazorAutoMapperProfile>(validate: true);
		});

		Configure<AbpNavigationOptions>(options =>
		{
			options.MenuContributors.Add(new TenantServiceMenuContributor());
		});

		Configure<AbpRouterOptions>(options =>
		{
			options.AdditionalAssemblies.Add(typeof(TenantServiceBlazorModule).Assembly);
		});
	}
}
