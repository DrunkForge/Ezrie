using Ezrie.Hosting.AspNetCore.Microservices;
using Ezrie.TenantService.EntityFrameworkCore;
using Volo.Abp;
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
		var hostingEnvironment = context.Services.GetHostingEnvironment();
		var configuration = context.Services.GetConfiguration();

		if (hostingEnvironment.IsDevelopment())
		{
			Configure<AbpVirtualFileSystemOptions>(options =>
			{
				options.FileSets.ReplaceEmbeddedByPhysical<TenantServiceDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Ezrie.TenantService.Domain.Shared", Path.DirectorySeparatorChar)));
				options.FileSets.ReplaceEmbeddedByPhysical<TenantServiceDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Ezrie.TenantService.Domain", Path.DirectorySeparatorChar)));
				options.FileSets.ReplaceEmbeddedByPhysical<TenantServiceApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Ezrie.TenantService.Application.Contracts", Path.DirectorySeparatorChar)));
				options.FileSets.ReplaceEmbeddedByPhysical<TenantServiceApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}Ezrie.TenantService.Application", Path.DirectorySeparatorChar)));
			});
		}

		Configure<AbpEndpointRouterOptions>(options =>
		{
			options.EndpointConfigureActions.Add(endpointContext =>
			{
				endpointContext.Endpoints.MapControllers();
				endpointContext.Endpoints.MapDefaultControllerRoute();
			});
		});
	}
}
