using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Volo.Abp.TenantManagement;

namespace Ezrie.TenantService;

[DependsOn(
    typeof(TenantServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
[DependsOn(typeof(AbpTenantManagementApplicationContractsModule))]
    public class TenantServiceApplicationContractsModule : AbpModule
{

}
