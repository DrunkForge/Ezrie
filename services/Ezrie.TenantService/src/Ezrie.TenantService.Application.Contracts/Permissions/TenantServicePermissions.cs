using Volo.Abp.Reflection;

namespace Ezrie.TenantService.Permissions;

public static class TenantServicePermissions
{
	public const String GroupName = "TenantService";

	public static String[] GetAll()
	{
		return ReflectionHelper.GetPublicConstantsRecursively(typeof(TenantServicePermissions));
	}
}
