using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Identity;

namespace Ezrie.IdentityService;

[DependsOn(typeof(EzrieDomainModule))]
[DependsOn(typeof(IdentityServiceDomainSharedModule))]

[DependsOn(typeof(AbpDddDomainModule))]
[DependsOn(typeof(AbpIdentityDomainModule))]
public class IdentityServiceDomainModule : AbpModule
{

}
