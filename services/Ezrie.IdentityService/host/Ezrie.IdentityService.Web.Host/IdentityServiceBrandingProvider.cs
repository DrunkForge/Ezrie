using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Ezrie.IdentityService;

[Dependency(ReplaceServices = true)]
public class IdentityServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "IdentityService";
}
