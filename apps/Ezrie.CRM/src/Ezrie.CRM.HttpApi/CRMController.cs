using Ezrie.CRM.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Ezrie.CRM;

public abstract class CRMController : AbpControllerBase
{
    protected CRMController()
    {
        LocalizationResource = typeof(CRMResource);
    }
}
