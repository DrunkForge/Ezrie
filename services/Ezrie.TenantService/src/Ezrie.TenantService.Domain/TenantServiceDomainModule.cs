using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Ezrie.TenantService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(TenantServiceDomainSharedModule)
)]
public class TenantServiceDomainModule : AbpModule
{

}
