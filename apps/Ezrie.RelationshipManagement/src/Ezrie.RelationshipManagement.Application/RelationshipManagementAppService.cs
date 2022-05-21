using System;
using System.Collections.Generic;
using System.Text;
using Ezrie.RelationshipManagement.Localization;
using Volo.Abp.Application.Services;

namespace Ezrie.RelationshipManagement;

/* Inherit your application services from this class.
 */
public abstract class RelationshipManagementAppService : ApplicationService
{
    protected RelationshipManagementAppService()
    {
        LocalizationResource = typeof(RelationshipManagementResource);
    }
}
