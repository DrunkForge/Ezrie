using Volo.Abp.Modularity;

namespace Ezrie.Admin;

[DependsOn(
    typeof(AdminApplicationModule),
    typeof(AdminDomainTestModule)
)]
public class AdminApplicationTestModule : AbpModule
{

}
