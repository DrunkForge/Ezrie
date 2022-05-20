using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using Ezrie.TenantService.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.TenantManagement;

namespace Ezrie.TenantService;

[DependsOn(typeof(EzrieDomainSharedModule))]
[DependsOn(typeof(AbpValidationModule))]
[DependsOn(typeof(AbpTenantManagementDomainSharedModule))]
    public class TenantServiceDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TenantServiceDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<TenantServiceResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/TenantService");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("TenantService", typeof(TenantServiceResource));
        });
    }
}
