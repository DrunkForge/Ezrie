using Ezrie.CRM.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Ezrie.CRM;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(CRMEntityFrameworkCoreTestModule)
    )]
public class CRMDomainTestModule : AbpModule
{

}
