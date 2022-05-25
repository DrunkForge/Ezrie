using Ezrie.IdentityService.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Ezrie.IdentityService.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class IdentityServicePageModel : AbpPageModel
{
    protected IdentityServicePageModel()
    {
        LocalizationResourceType = typeof(IdentityServiceResource);
        ObjectMapperContext = typeof(IdentityServiceWebModule);
    }
}
