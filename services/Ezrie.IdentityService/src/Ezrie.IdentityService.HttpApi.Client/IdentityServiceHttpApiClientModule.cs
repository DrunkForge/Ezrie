using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Identity;
using Volo.Abp.Account;

namespace Ezrie.IdentityService;

[DependsOn(typeof(IdentityServiceApplicationContractsModule))]
[DependsOn(typeof(AbpHttpClientModule))]
[DependsOn(typeof(AbpAccountHttpApiClientModule))]
[DependsOn(typeof(AbpIdentityHttpApiClientModule))]
public class IdentityServiceHttpApiClientModule : AbpModule
{
	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddHttpClientProxies(
			typeof(IdentityServiceApplicationContractsModule).Assembly,
			IdentityServiceRemoteServiceConsts.RemoteServiceName
		);

		Configure<AbpVirtualFileSystemOptions>(options =>
		{
			options.FileSets.AddEmbedded<IdentityServiceHttpApiClientModule>();
		});

	}
}
