using Ezrie.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Ezrie.CRM.EntityFrameworkCore.Migrations;

[DependsOn(typeof(EzrieEntityFrameworkCoreMigrationsModule))]
[DependsOn(typeof(CRMApplicationContractsModule))]
[DependsOn(typeof(CRMEntityFrameworkCoreModule))]
public class CRMEntityFrameworkCoreMigrationsModule : MigrationsModuleBase
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAbpDbContext<CRMMigrationsDbContext>(options =>
		{
		});
	}
}
