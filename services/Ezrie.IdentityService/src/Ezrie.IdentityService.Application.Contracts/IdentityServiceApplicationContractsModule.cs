using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Volo.Abp.Identity;

namespace Ezrie.IdentityService;

[DependsOn(typeof(IdentityServiceDomainSharedModule))]
[DependsOn(typeof(AbpDddApplicationContractsModule))]
[DependsOn(typeof(AbpAuthorizationModule))]
[DependsOn(typeof(AbpIdentityApplicationContractsModule))]
public class IdentityServiceApplicationContractsModule : AbpModule
{

}
