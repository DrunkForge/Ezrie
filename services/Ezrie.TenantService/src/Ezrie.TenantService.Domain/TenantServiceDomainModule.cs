using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;

namespace Ezrie.TenantService;

[DependsOn(typeof(AbpDddDomainModule))]
[DependsOn(typeof(TenantServiceDomainSharedModule))]
[DependsOn(typeof(AbpTenantManagementDomainModule))]
[DependsOn(typeof(EzrieDomainModule))]
public class TenantServiceDomainModule : AbpModule
{

}
