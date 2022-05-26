using Ezrie.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Volo.Abp.EntityFrameworkCore;
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
