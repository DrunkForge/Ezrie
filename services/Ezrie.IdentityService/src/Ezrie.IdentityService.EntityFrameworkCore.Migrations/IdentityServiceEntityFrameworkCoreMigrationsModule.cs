using Ezrie.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Ezrie.IdentityService.EntityFrameworkCore;

[DependsOn(typeof(IdentityServiceApplicationContractsModule))]
[DependsOn(typeof(IdentityServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(EzrieEntityFrameworkCoreMigrationsModule))]
public class IdentityServiceEntityFrameworkCoreMigrationsModule : MigrationsModuleBase
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAbpDbContext<IdentityServiceMigrationsDbContext>(options =>
		{
		});
	}
}
