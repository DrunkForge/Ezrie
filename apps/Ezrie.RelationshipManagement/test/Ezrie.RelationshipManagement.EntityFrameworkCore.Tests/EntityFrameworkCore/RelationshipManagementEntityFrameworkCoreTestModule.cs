using Ezrie.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace Ezrie.RelationshipManagement.EntityFrameworkCore;

[DependsOn(typeof(RelationshipManagementEntityFrameworkCoreModule))]
[DependsOn(typeof(RelationshipManagementTestBaseModule))]
[DependsOn(typeof(AbpEntityFrameworkCoreSqliteModule))]
public class RelationshipManagementEntityFrameworkCoreTestModule : EzrieEntityFrameworkCoreTestModuleBase<RelationshipManagementDbContext>
{
	public override RelationshipManagementDbContext CreateDbContext(DbContextOptions<RelationshipManagementDbContext> options) => new(options);
}
