using Ezrie.IdentityService.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Ezrie.IdentityService.Pages;

public abstract class IdentityServicePageModel : AbpPageModel
{
    protected IdentityServicePageModel()
    {
        LocalizationResourceType = typeof(IdentityServiceResource);
    }
}
