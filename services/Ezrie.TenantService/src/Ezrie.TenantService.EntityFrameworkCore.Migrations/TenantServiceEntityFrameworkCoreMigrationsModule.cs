using Ezrie.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Ezrie.TenantService.EntityFrameworkCore.Migrations;

[DependsOn(typeof(TenantServiceApplicationContractsModule))]
[DependsOn(typeof(TenantServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(EzrieEntityFrameworkCoreMigrationsModule))]
public class TenantServiceEntityFrameworkCoreMigrationsModule : MigrationsModuleBase
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAbpDbContext<TenantServiceMigrationsDbContext>();
	}
}
