using Microsoft.Extensions.DependencyInjection;
using Ezrie.TenantService.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.TenantManagement.Blazor.Server;

namespace Ezrie.TenantService.Blazor;

[DependsOn(
    typeof(TenantServiceApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
[DependsOn(typeof(AbpTenantManagementBlazorServerModule))]
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
