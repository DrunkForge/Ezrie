using Ezrie.RelationshipManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Ezrie.RelationshipManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(RelationshipManagementEntityFrameworkCoreModule),
    typeof(RelationshipManagementApplicationContractsModule)
    )]
public class RelationshipManagementDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
