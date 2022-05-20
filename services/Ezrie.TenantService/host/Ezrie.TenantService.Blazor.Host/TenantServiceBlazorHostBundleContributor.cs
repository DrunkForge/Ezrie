using Volo.Abp.Bundling;

namespace Ezrie.TenantService.Blazor.Host;

public class TenantServiceBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
