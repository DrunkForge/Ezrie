using Ezrie.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Ezrie.TenantService.EntityFrameworkCore;

[DependsOn(typeof(EzrieEntityFrameworkCoreModule))]
[DependsOn(typeof(TenantServiceDomainModule))]
[DependsOn(typeof(AbpTenantManagementEntityFrameworkCoreModule))]
public class TenantServiceEntityFrameworkCoreModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAbpDbContext<TenantServiceDbContext>(options =>
		{
			/* Add custom repositories here. Example:
			 * options.AddRepository<Question, EfCoreQuestionRepository>();
			 */
		});
	}
}
