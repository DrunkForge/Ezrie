using Ezrie.TenantService.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Ezrie.TenantService.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class TenantServicePageModel : AbpPageModel
{
    protected TenantServicePageModel()
    {
        LocalizationResourceType = typeof(TenantServiceResource);
        ObjectMapperContext = typeof(TenantServiceWebModule);
    }
}
