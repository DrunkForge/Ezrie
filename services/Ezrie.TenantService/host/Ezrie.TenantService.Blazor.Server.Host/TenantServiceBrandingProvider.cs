using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Ezrie.TenantService.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class TenantServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TenantService";
}
