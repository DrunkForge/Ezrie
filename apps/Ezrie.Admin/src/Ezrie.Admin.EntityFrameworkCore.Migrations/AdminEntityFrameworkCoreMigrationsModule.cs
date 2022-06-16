using Ezrie.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Ezrie.Admin.EntityFrameworkCore;

[DependsOn(typeof(EzrieEntityFrameworkCoreMigrationsModule))]
[DependsOn(typeof(AdminApplicationContractsModule))]
[DependsOn(typeof(AdminEntityFrameworkCoreModule))]
public class AdminEntityFrameworkCoreMigrationsModule : MigrationsModuleBase
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAbpDbContext<AdminMigrationsDbContext>(options =>
		{
		});
	}
}
