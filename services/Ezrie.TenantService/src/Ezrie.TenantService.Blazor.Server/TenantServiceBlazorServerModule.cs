using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace Ezrie.TenantService;

[DependsOn(typeof(AbpAspNetCoreComponentsServerThemingModule))]
[DependsOn(typeof(TenantServiceBlazorModule))]
public class TenantServiceBlazorServerModule : AbpModule
{

}
