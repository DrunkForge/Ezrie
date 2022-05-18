
using Microsoft.Extensions.DependencyInjection;
using Ezrie.AdministrationService.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace Ezrie.AdministrationService.Blazor;

[DependsOn(typeof(AdministrationServiceApplicationContractsModule))]
[DependsOn(typeof(AbpAspNetCoreComponentsWebThemingModule))]
[DependsOn(typeof(AbpAutoMapperModule))]
public class AdministrationServiceBlazorModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAutoMapperObjectMapper<AdministrationServiceBlazorModule>();

		Configure<AbpAutoMapperOptions>(options =>
		{
			options.AddProfile<AdministrationServiceBlazorAutoMapperProfile>(validate: true);
		});

		Configure<AbpNavigationOptions>(options =>
		{
			options.MenuContributors.Add(new AdministrationServiceMenuContributor());
		});

		Configure<AbpRouterOptions>(options =>
		{
			options.AdditionalAssemblies.Add(typeof(AdministrationServiceBlazorModule).Assembly);
		});
	}
}
