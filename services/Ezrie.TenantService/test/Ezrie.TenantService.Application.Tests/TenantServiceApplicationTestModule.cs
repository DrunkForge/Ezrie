using Volo.Abp.Modularity;

namespace Ezrie.TenantService;

[DependsOn(
    typeof(TenantServiceApplicationModule),
    typeof(TenantServiceDomainTestModule)
    )]
public class TenantServiceApplicationTestModule : AbpModule
{

}
