using Ezrie.Hosting.AspNetCore;
using Ezrie.Hosting.AspNetCore.Microservices;
using Ezrie.TenantService.EntityFrameworkCore;
using System.Globalization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Ezrie.TenantService;

[DependsOn(typeof(EzrieHostingAspNetCoreMicroservicesModule))]
[DependsOn(typeof(TenantServiceApplicationModule))]
[DependsOn(typeof(TenantServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(TenantServiceHttpApiModule))]
public class TenantServiceHttpApiHostModule : AbpModule
{

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		ConfigureConventionalControllers();
		ConfigureVirtualFileSystem(context);

		context.ConfigureJwtAuthentication(TenantServiceOidcProperties.Audience);
	}

	private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
	{
		var hostingEnvironment = context.Services.GetHostingEnvironment();

		if (hostingEnvironment.IsDevelopment())
		{
			Configure<AbpVirtualFileSystemOptions>(options =>
			{
				options.FileSets.ReplaceEmbeddedByPhysical<TenantServiceDomainSharedModule>(
					Path.Combine(hostingEnvironment.ContentRootPath, String.Format(CultureInfo.InvariantCulture,
					"..{0}..{0}src{0}Ezrie.TenantService.Domain.Shared", Path.DirectorySeparatorChar)));
				options.FileSets.ReplaceEmbeddedByPhysical<TenantServiceDomainModule>(
					Path.Combine(hostingEnvironment.ContentRootPath, String.Format(CultureInfo.InvariantCulture,
					"..{0}..{0}src{0}Ezrie.TenantService.Domain", Path.DirectorySeparatorChar)));
				options.FileSets.ReplaceEmbeddedByPhysical<TenantServiceApplicationContractsModule>(
					Path.Combine(hostingEnvironment.ContentRootPath, String.Format(CultureInfo.InvariantCulture,
					"..{0}..{0}src{0}Ezrie.TenantService.Application.Contracts", Path.DirectorySeparatorChar)));
				options.FileSets.ReplaceEmbeddedByPhysical<TenantServiceApplicationModule>(
					Path.Combine(hostingEnvironment.ContentRootPath, String.Format(CultureInfo.InvariantCulture,
					"..{0}..{0}src{0}Ezrie.TenantService.Application", Path.DirectorySeparatorChar)));
			});
		}
	}

	private void ConfigureConventionalControllers()
	{
		Configure<AbpAspNetCoreMvcOptions>(options =>
		{
			options.ConventionalControllers.Create(typeof(TenantServiceHttpApiHostModule).Assembly);
			options.ConventionalControllers.Create(typeof(TenantServiceApplicationModule).Assembly);
		});
	}
}
