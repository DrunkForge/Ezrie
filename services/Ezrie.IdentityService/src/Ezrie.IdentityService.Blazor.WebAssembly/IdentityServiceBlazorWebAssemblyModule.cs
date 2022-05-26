using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Ezrie.IdentityService;

[DependsOn(typeof(IdentityServiceBlazorModule))]
[DependsOn(typeof(IdentityServiceHttpApiClientModule))]
[DependsOn(typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule))]
public class IdentityServiceBlazorWebAssemblyModule : AbpModule
{

}
