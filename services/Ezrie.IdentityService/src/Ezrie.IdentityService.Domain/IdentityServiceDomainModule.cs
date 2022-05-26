using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement.Identity;

namespace Ezrie.IdentityService;

[DependsOn(typeof(EzrieDomainModule))]
[DependsOn(typeof(AbpDddDomainModule))]
[DependsOn(typeof(IdentityServiceDomainSharedModule))]
[DependsOn(typeof(AbpIdentityDomainModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityModule))]
public class IdentityServiceDomainModule : AbpModule
{

}
