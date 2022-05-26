using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Ezrie.IdentityService.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Identity;

namespace Ezrie.IdentityService;

[DependsOn(typeof(EzrieDomainSharedModule))]
[DependsOn(typeof(AbpValidationModule))]
[DependsOn(typeof(AbpIdentityDomainSharedModule))]
public class IdentityServiceDomainSharedModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpVirtualFileSystemOptions>(options =>
		{
			options.FileSets.AddEmbedded<IdentityServiceDomainSharedModule>();
		});

		Configure<AbpLocalizationOptions>(options =>
		{
			options.Resources
				.Add<IdentityServiceResource>("en")
				.AddBaseTypes(typeof(AbpValidationResource))
				.AddVirtualJson("/Localization/IdentityService");
		});

		Configure<AbpExceptionLocalizationOptions>(options =>
		{
			options.MapCodeNamespace("IdentityService", typeof(IdentityServiceResource));
		});
	}
}
