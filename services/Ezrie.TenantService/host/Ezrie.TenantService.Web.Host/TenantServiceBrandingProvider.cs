using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Ezrie.TenantService;

[Dependency(ReplaceServices = true)]
public class TenantServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TenantService";
}
