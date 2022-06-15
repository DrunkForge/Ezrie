using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Volo.Abp.Identity;
using Volo.Abp.Account;

namespace Ezrie.IdentityService;

[DependsOn(typeof(IdentityServiceDomainModule))]
[DependsOn(typeof(IdentityServiceApplicationContractsModule))]
[DependsOn(typeof(AbpDddApplicationModule))]
[DependsOn(typeof(AbpAutoMapperModule))]
[DependsOn(typeof(AbpAccountApplicationModule))]
[DependsOn(typeof(AbpIdentityApplicationModule))]
public class IdentityServiceApplicationModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAutoMapperObjectMapper<IdentityServiceApplicationModule>();
		Configure<AbpAutoMapperOptions>(options =>
		{
			options.AddMaps<IdentityServiceApplicationModule>(validate: true);
		});
	}
}
