using Ezrie.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Ezrie.RelationshipManagement.EntityFrameworkCore.Migrations;

[DependsOn(typeof(EzrieEntityFrameworkCoreMigrationsModule))]
[DependsOn(typeof(RelationshipManagementEntityFrameworkCoreModule))]
public class RelationshipManagementEntityFrameworkCoreMigrationsModule : MigrationsModuleBase
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAbpDbContext<RelationshipManagementMigrationsDbContext>(options =>
		{
		});
	}
}
