using Ezrie.Admin.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Ezrie.Admin;

[DependsOn(
    typeof(AdminEntityFrameworkCoreTestModule)
)]
public class AdminDomainTestModule : AbpModule
{

}
