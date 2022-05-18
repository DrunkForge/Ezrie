
using Volo.Abp.Modularity;

namespace Ezrie.AdministrationService;

[DependsOn(typeof(AdministrationServiceApplicationModule))]
[DependsOn(typeof(AdministrationServiceDomainTestModule))]
public class AdministrationServiceApplicationTestModule : AbpModule
{

}
