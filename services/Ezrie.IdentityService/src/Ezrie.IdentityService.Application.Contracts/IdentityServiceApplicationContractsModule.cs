using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;
using Volo.Abp.Identity;
using Volo.Abp.Account;

namespace Ezrie.IdentityService;

[DependsOn(typeof(IdentityServiceDomainSharedModule))]
[DependsOn(typeof(AbpDddApplicationContractsModule))]
[DependsOn(typeof(AbpAuthorizationModule))]
[DependsOn(typeof(AbpAccountApplicationContractsModule))]
[DependsOn(typeof(AbpIdentityApplicationContractsModule))]
public class IdentityServiceApplicationContractsModule : AbpModule
{

}
