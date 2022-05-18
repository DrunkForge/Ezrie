
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Ezrie.AppSettings;

[DependsOn(typeof(AbpAutofacModule))]
public partial class EzrieAppSettingsModule : AbpModule
{
}
