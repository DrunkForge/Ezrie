
using Ezrie.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace Ezrie.AdministrationService.EntityFrameworkCore;

[DependsOn(typeof(AdministrationServiceTestBaseModule))]
[DependsOn(typeof(AdministrationServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpEntityFrameworkCoreSqliteModule))]
public class AdministrationServiceEntityFrameworkCoreTestModule : EzrieEntityFrameworkCoreTestModuleBase<AdministrationServiceDbContext>
{
	public override AdministrationServiceDbContext CreateDbContext(DbContextOptions<AdministrationServiceDbContext> options) => new(options);
}
