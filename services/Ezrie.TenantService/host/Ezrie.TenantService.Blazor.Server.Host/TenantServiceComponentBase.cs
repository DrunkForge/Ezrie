using Ezrie.TenantService.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Ezrie.TenantService.Blazor.Server.Host;

public abstract class TenantServiceComponentBase : AbpComponentBase
{
    protected TenantServiceComponentBase()
    {
        LocalizationResource = typeof(TenantServiceResource);
    }
}
