using Ezrie.Admin.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Ezrie.Admin.Blazor;

public abstract class AdminComponentBase : AbpComponentBase
{
    protected AdminComponentBase()
    {
        LocalizationResource = typeof(AdminResource);
    }
}
