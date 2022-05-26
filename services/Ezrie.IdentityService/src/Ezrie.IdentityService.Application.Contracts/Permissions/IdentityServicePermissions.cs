using Volo.Abp.Reflection;

namespace Ezrie.IdentityService.Permissions;

public static class IdentityServicePermissions
{
    public const String GroupName = "IdentityService";

    public static String[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(IdentityServicePermissions));
    }
}
