using Ezrie.CRM.Localization;
using Volo.Abp.AuditLogging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Ezrie.CRM;

[DependsOn(typeof(AbpAuditLoggingDomainSharedModule))]
[DependsOn(typeof(AbpBackgroundJobsDomainSharedModule))]
[DependsOn(typeof(AbpFeatureManagementDomainSharedModule))]
[DependsOn(typeof(AbpIdentityDomainSharedModule))]
[DependsOn(typeof(AbpIdentityServerDomainSharedModule))]
[DependsOn(typeof(AbpPermissionManagementDomainSharedModule))]
[DependsOn(typeof(AbpSettingManagementDomainSharedModule))]
[DependsOn(typeof(AbpTenantManagementDomainSharedModule))]
public class CrmDomainSharedModule : AbpModule
{
	public override void PreConfigureServices(ServiceConfigurationContext context)
	{
		CrmGlobalFeatureConfigurator.Configure();
		CrmModuleExtensionConfigurator.Configure();
	}

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpVirtualFileSystemOptions>(options =>
		{
			options.FileSets.AddEmbedded<CrmDomainSharedModule>();
		});

		Configure<AbpLocalizationOptions>(options =>
		{
			options.Resources
				.Add<CrmResource>("en")
				.AddBaseTypes(typeof(AbpValidationResource))
				.AddVirtualJson("/Localization/CRM");

			options.DefaultResourceType = typeof(CrmResource);
		});

		Configure<AbpExceptionLocalizationOptions>(options =>
		{
			options.MapCodeNamespace("CRM", typeof(CrmResource));
		});
	}
}
