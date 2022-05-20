using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace Ezrie.TenantService;

[DependsOn(
    typeof(TenantServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class TenantServiceApplicationContractsModule : AbpModule
{

}
