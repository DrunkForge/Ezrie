using Ezrie.CRM.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ezrie.CRM.Permissions;

public class CrmPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CrmPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(CrmPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(String name)
    {
        return LocalizableString.Create<CrmResource>(name);
    }
}
