using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Ezrie.IdentityService.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class IdentityServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "IdentityService";
}
