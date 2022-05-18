
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Ezrie.AdministrationService;

[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AdministrationServiceHttpApiClientModule))]
[DependsOn(typeof(AbpHttpClientIdentityModelModule))]
public class AdministrationServiceConsoleApiClientModule : AbpModule
{

}
