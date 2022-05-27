using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Ezrie.TenantService;

[DependsOn(typeof(EzrieDomainModule))]
[DependsOn(typeof(TenantServiceDomainSharedModule))]

[DependsOn(typeof(AbpDddDomainModule))]
[DependsOn(typeof(AbpTenantManagementDomainModule))]
public class TenantServiceDomainModule : AbpModule
{

}
