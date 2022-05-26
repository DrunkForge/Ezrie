using Ezrie.IdentityService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ezrie.IdentityService.Permissions;

public class IdentityServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(IdentityServicePermissions.GroupName, L("Permission:IdentityService"));
    }

    private static LocalizableString L(String name)
    {
        return LocalizableString.Create<IdentityServiceResource>(name);
    }
}
