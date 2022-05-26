using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Ezrie.IdentityService.EntityFrameworkCore;

[DependsOn(typeof(IdentityServiceDomainModule))]

[DependsOn(typeof(AbpEntityFrameworkCoreModule))]
[DependsOn(typeof(AbpIdentityEntityFrameworkCoreModule))]
public class IdentityServiceEntityFrameworkCoreModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAbpDbContext<IdentityServiceDbContext>(options =>
		{
			options.ReplaceDbContext<IIdentityDbContext>();

			options.AddDefaultRepositories(true);
		});
	}
}
