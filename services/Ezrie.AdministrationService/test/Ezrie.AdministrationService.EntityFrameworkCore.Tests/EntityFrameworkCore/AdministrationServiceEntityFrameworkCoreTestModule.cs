
using Ezrie.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace Ezrie.AdministrationService.EntityFrameworkCore;

[DependsOn(typeof(AdministrationServiceTestBaseModule))]
[DependsOn(typeof(AdministrationServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpEntityFrameworkCoreSqliteModule))]
public class AdministrationServiceEntityFrameworkCoreTestModule : EzrieEntityFrameworkCoreTestModule<AdministrationServiceDbContext>
{
	public override AdministrationServiceDbContext CreateDbContext(DbContextOptions<AdministrationServiceDbContext> options) => new (options);
}
