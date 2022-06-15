using Volo.Abp.Modularity;

namespace Ezrie.CRM;

[DependsOn(
    typeof(CRMApplicationModule),
    typeof(CRMDomainTestModule)
    )]
public class CRMApplicationTestModule : AbpModule
{

}
