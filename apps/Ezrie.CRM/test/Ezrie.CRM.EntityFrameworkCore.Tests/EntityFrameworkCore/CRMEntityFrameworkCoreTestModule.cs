using Ezrie.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace Ezrie.CRM.EntityFrameworkCore;

[DependsOn(typeof(CRMTestBaseModule))]
[DependsOn(typeof(CRMEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpEntityFrameworkCoreSqliteModule))]
public class CRMEntityFrameworkCoreTestModule : EzrieEntityFrameworkCoreTestModuleBase<CRMDbContext>
{
	public override CRMDbContext CreateDbContext(DbContextOptions<CRMDbContext> options) => new(options);
}
