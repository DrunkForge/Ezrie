using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Ezrie.TenantService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(TenantServiceDomainSharedModule)
)]
[DependsOn(typeof(AbpTenantManagementDomainModule))]
    public class TenantServiceDomainModule : AbpModule
{

}
