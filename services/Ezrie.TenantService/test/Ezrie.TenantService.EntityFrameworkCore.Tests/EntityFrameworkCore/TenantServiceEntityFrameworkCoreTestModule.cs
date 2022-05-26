using Ezrie.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace Ezrie.TenantService.EntityFrameworkCore;

[DependsOn(typeof(TenantServiceTestBaseModule))]
[DependsOn(typeof(TenantServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpEntityFrameworkCoreSqliteModule))]
public class TenantServiceEntityFrameworkCoreTestModule : EzrieEntityFrameworkCoreTestModuleBase<TenantServiceDbContext>
{
	public override TenantServiceDbContext CreateDbContext(DbContextOptions<TenantServiceDbContext> options) => new(options);
}
