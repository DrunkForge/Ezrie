using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Ezrie.CRM;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CRMHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class CRMConsoleApiClientModule : AbpModule
{

}
