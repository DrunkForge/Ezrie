using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Ezrie.IdentityService.Blazor.WebAssembly;

[DependsOn(
    typeof(IdentityServiceBlazorModule),
    typeof(IdentityServiceHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class IdentityServiceBlazorWebAssemblyModule : AbpModule
{

}
