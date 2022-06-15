using Volo.Abp.Bundling;

namespace Ezrie.CRM.Blazor.Host;

public class CRMBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
