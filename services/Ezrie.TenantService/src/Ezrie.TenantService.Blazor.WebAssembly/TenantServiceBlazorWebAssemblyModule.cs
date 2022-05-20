using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace Ezrie.TenantService;

[DependsOn(typeof(TenantServiceBlazorModule))]
[DependsOn(typeof(TenantServiceHttpApiClientModule))]
[DependsOn(typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule))]
public class TenantServiceBlazorWebAssemblyModule : AbpModule
{

}
