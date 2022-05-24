using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.TenantManagement;

namespace Ezrie.TenantService;

[DependsOn(
	typeof(TenantServiceDomainModule),
	typeof(TenantServiceApplicationContractsModule),
	typeof(AbpDddApplicationModule),
	typeof(AbpAutoMapperModule)
	)]
[DependsOn(typeof(AbpTenantManagementApplicationModule))]
public class TenantServiceApplicationModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAutoMapperObjectMapper<TenantServiceApplicationModule>();
		Configure<AbpAutoMapperOptions>(options =>
		{
			options.AddMaps<TenantServiceApplicationModule>(validate: true);
		});
	}
}
