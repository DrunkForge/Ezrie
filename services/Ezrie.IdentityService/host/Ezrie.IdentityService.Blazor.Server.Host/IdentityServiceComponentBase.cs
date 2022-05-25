using Ezrie.IdentityService.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Ezrie.IdentityService.Blazor.Server.Host;

public abstract class IdentityServiceComponentBase : AbpComponentBase
{
    protected IdentityServiceComponentBase()
    {
        LocalizationResource = typeof(IdentityServiceResource);
    }
}
