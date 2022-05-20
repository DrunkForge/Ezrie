using Ezrie.TenantService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Ezrie.TenantService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(TenantServiceEntityFrameworkCoreTestModule)
    )]
public class TenantServiceDomainTestModule : AbpModule
{

}
