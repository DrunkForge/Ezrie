using Ezrie.TenantService.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Ezrie.TenantService.Pages;

public abstract class TenantServicePageModel : AbpPageModel
{
    protected TenantServicePageModel()
    {
        LocalizationResourceType = typeof(TenantServiceResource);
    }
}
