using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Ezrie.TenantService;

[DependsOn(
	typeof(AbpAutofacModule),
	typeof(TenantServiceHttpApiClientModule),
	typeof(AbpHttpClientIdentityModelModule)
	)]
public class TenantServiceConsoleApiClientModule : AbpModule
{

}
