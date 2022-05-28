using Ezrie.MultiTenancy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Emailing;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.PermissionManagement.IdentityServer;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;

namespace Ezrie.CRM;

[DependsOn(typeof(CrmDomainSharedModule))]
[DependsOn(typeof(AbpAuditLoggingDomainModule))]
[DependsOn(typeof(AbpBackgroundJobsDomainModule))]
[DependsOn(typeof(AbpFeatureManagementDomainModule))]
[DependsOn(typeof(AbpIdentityDomainModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityModule))]
[DependsOn(typeof(AbpIdentityServerDomainModule))]
[DependsOn(typeof(AbpPermissionManagementDomainIdentityServerModule))]
[DependsOn(typeof(AbpSettingManagementDomainModule))]
[DependsOn(typeof(AbpTenantManagementDomainModule))]
[DependsOn(typeof(AbpEmailingModule))]
public class CrmDomainModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpMultiTenancyOptions>(options =>
		{
			options.IsEnabled = MultiTenancyConsts.IsEnabled;
		});

#if DEBUG
		context.Services.Replace(ServiceDescriptor.Singleton<IEmailSender, NullEmailSender>());
#endif
	}
}
