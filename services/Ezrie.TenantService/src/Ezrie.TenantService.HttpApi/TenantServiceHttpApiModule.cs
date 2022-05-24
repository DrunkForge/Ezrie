using Localization.Resources.AbpUi;
using Ezrie.TenantService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.TenantManagement;

namespace Ezrie.TenantService;

[DependsOn(
	typeof(TenantServiceApplicationContractsModule),
	typeof(AbpAspNetCoreMvcModule))]
[DependsOn(typeof(AbpTenantManagementHttpApiModule))]
public class TenantServiceHttpApiModule : AbpModule
{
	public override void PreConfigureServices(ServiceConfigurationContext context)
	{
		PreConfigure<IMvcBuilder>(mvcBuilder =>
		{
			mvcBuilder.AddApplicationPartIfNotExists(typeof(TenantServiceHttpApiModule).Assembly);
		});
	}

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		Configure<AbpLocalizationOptions>(options =>
		{
			options.Resources
				.Get<TenantServiceResource>()
				.AddBaseTypes(typeof(AbpUiResource));
		});
	}
}
