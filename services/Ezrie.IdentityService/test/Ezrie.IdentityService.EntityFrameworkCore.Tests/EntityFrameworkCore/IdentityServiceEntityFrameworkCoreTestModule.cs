using Ezrie.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace Ezrie.IdentityService.EntityFrameworkCore;

[DependsOn(typeof(IdentityServiceTestBaseModule))]
[DependsOn(typeof(IdentityServiceEntityFrameworkCoreModule))]
[DependsOn(	typeof(AbpEntityFrameworkCoreSqliteModule))]
public class IdentityServiceEntityFrameworkCoreTestModule : EzrieEntityFrameworkCoreTestModuleBase<IdentityServiceDbContext>
{
	public override IdentityServiceDbContext CreateDbContext(DbContextOptions<IdentityServiceDbContext> options) => new(options);
}
