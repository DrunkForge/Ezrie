using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Ezrie.RelationshipManagement.Blazor;

[Dependency(ReplaceServices = true)]
public class RelationshipManagementBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "RelationshipManagement";
}
