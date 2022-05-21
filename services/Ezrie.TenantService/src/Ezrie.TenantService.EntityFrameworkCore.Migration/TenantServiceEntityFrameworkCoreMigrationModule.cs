using Ezrie.EntityFrameworkCore.Migrations;
using Ezrie.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp.Modularity;

namespace Ezrie.TenantService.EntityFrameworkCore.Migration;

[DependsOn(typeof(TenantServiceApplicationContractsModule))]
[DependsOn(typeof(TenantServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(EzrieEntityFrameworkCoreMigrationsModule))]
public class TenantServiceEntityFrameworkCoreMigrationModule : MigrationModuleBase
{
}
