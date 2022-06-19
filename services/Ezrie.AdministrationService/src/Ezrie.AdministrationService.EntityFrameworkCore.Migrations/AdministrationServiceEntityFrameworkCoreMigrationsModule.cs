using Ezrie.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Ezrie.AdministrationService.EntityFrameworkCore;

[DependsOn(typeof(EzrieEntityFrameworkCoreMigrationsModule))]
[DependsOn(typeof(AdministrationServiceApplicationContractsModule))]
[DependsOn(typeof(AdministrationServiceEntityFrameworkCoreModule))]
public class AdministrationServiceEntityFrameworkCoreMigrationsModule : MigrationsModuleBase
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAbpDbContext<AdministrationServiceMigrationsDbContext>(options =>
		{
		});
	}
}
