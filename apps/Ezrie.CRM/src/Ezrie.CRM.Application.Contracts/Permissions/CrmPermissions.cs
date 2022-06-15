using Volo.Abp.Reflection;

namespace Ezrie.CRM.Permissions;

public static class CRMPermissions
{
    public const String GroupName = "CRM";

    public static String[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CRMPermissions));
    }
}
