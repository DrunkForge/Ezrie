﻿
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Authorization;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Ezrie.AdministrationService;

[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpTestBaseModule))]
[DependsOn(typeof(AbpAuthorizationModule))]
[DependsOn(typeof(AdministrationServiceDomainModule))]
public class AdministrationServiceTestBaseModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddAlwaysAllowAuthorization();
	}

	public override void OnApplicationInitialization(ApplicationInitializationContext context)
	{
		SeedTestData(context);
	}

	private static void SeedTestData(ApplicationInitializationContext context)
	{
		AsyncHelper.RunSync(async () =>
		{
			using (var scope = context.ServiceProvider.CreateScope())
			{
				await scope.ServiceProvider
					.GetRequiredService<IDataSeeder>()
					.SeedAsync();
			}
		});
	}
}
