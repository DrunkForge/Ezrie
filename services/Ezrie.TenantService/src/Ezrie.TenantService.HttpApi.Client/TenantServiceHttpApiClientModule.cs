using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Ezrie.TenantService;

[DependsOn(
    typeof(TenantServiceApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class TenantServiceHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(TenantServiceApplicationContractsModule).Assembly,
            TenantServiceRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TenantServiceHttpApiClientModule>();
        });

    }
}
