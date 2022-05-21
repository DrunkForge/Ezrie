using Ezrie.RelationshipManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ezrie.RelationshipManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class RelationshipManagementController : AbpControllerBase
{
    protected RelationshipManagementController()
    {
        LocalizationResource = typeof(RelationshipManagementResource);
    }
}
