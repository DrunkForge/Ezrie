using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace Ezrie.IdentityService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(IdentityServiceDomainSharedModule)
)]
[DependsOn(typeof(AbpIdentityDomainModule))]
    [DependsOn(typeof(AbpPermissionManagementDomainIdentityModule))]
    public class IdentityServiceDomainModule : AbpModule
{

}
