using Ezrie.RelationshipManagement.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Ezrie.RelationshipManagement.Blazor;

public abstract class RelationshipManagementComponentBase : AbpComponentBase
{
    protected RelationshipManagementComponentBase()
    {
        LocalizationResource = typeof(RelationshipManagementResource);
    }
}
