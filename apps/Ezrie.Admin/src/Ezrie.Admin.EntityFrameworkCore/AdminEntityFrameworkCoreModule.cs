using Ezrie.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Ezrie.Admin;

[DependsOn(typeof(AdminDomainModule))]
[DependsOn(typeof(EzrieEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpAuditLoggingEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpBackgroundJobsEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpFeatureManagementEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpIdentityEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpIdentityServerEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpPermissionManagementEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpSettingManagementEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpTenantManagementEntityFrameworkCoreModule))]
public class AdminEntityFrameworkCoreModule : AbpModule
{
	public override void PreConfigureServices(ServiceConfigurationContext context)
	{
		AdminEfCoreEntityExtensionMappings.Configure();
	}

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAbpDbContext<AdminDbContext>(options =>
		{
			/* Remove "includeAllEntities: true" to create
			 * default repositories only for aggregate roots */
			options.AddDefaultRepositories(includeAllEntities: true);
		});
	}
}
