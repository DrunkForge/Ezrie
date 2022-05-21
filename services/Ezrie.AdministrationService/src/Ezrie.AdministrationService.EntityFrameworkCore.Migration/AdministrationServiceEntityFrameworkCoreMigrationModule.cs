using Ezrie.EntityFrameworkCore.Migrations;
using Ezrie.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.Modularity;

namespace Ezrie.AdministrationService.EntityFrameworkCore.Migration;

[DependsOn(typeof(EzrieEntityFrameworkCoreMigrationsModule))]
[DependsOn(typeof(AdministrationServiceApplicationContractsModule))]
[DependsOn(typeof(AdministrationServiceEntityFrameworkCoreModule))]
public class AdministrationServiceEntityFrameworkCoreMigrationModule : MigrationModuleBase
{
}
