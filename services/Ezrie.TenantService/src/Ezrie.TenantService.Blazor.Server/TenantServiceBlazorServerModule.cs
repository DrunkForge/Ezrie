using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Ezrie.TenantService.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(TenantServiceBlazorModule)
    )]
public class TenantServiceBlazorServerModule : AbpModule
{

}
