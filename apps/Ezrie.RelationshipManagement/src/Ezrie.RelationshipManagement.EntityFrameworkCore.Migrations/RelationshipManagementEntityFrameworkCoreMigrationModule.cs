using Ezrie.EntityFrameworkCore.Migrations;
using Volo.Abp.Modularity;

namespace Ezrie.RelationshipManagement.EntityFrameworkCore.Migrations;

[DependsOn(typeof(RelationshipManagementEntityFrameworkCoreModule))]
[DependsOn(typeof(EzrieEntityFrameworkCoreMigrationsModule))]
public class RelationshipManagementEntityFrameworkCoreMigrationsModule : MigrationsModuleBase
{

}
