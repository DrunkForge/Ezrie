using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.VirtualFileSystem;

namespace Ezrie.CRM;

[DependsOn(typeof(CrmApplicationContractsModule))]
[DependsOn(typeof(AbpAccountHttpApiClientModule))]
[DependsOn(typeof(AbpFeatureManagementHttpApiClientModule))]
[DependsOn(typeof(AbpIdentityHttpApiClientModule))]
[DependsOn(typeof(AbpPermissionManagementHttpApiClientModule))]
[DependsOn(typeof(AbpSettingManagementHttpApiClientModule))]
[DependsOn(typeof(AbpTenantManagementHttpApiClientModule))]
public class CrmHttpApiClientModule : AbpModule
{
	public const String RemoteServiceName = "Default";

	public override void ConfigureServices(ServiceConfigurationContext context)
	{
		context.Services.AddHttpClientProxies(typeof(CrmApplicationContractsModule).Assembly, RemoteServiceName);

		Configure<AbpVirtualFileSystemOptions>(options =>
		{
			options.FileSets.AddEmbedded<CrmHttpApiClientModule>();
		});
	}
}
