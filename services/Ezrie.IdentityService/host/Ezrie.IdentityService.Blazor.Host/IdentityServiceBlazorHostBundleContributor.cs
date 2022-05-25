using Volo.Abp.Bundling;

namespace Ezrie.IdentityService.Blazor.Host;

public class IdentityServiceBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
