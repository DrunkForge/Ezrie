using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Ezrie.Admin;

[Dependency(ReplaceServices = true)]
public class AdminBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Admin";
}
