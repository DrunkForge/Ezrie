using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Ezrie.TenantService.Blazor.WebAssembly;

[DependsOn(
    typeof(TenantServiceBlazorModule),
    typeof(TenantServiceHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class TenantServiceBlazorWebAssemblyModule : AbpModule
{

}
