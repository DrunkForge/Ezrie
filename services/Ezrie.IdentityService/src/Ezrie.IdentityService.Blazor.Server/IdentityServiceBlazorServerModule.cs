using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Ezrie.IdentityService.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(IdentityServiceBlazorModule)
    )]
public class IdentityServiceBlazorServerModule : AbpModule
{

}
