﻿using Ezrie.TenantService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Ezrie.TenantService.Permissions;

public class TenantServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
	public override void Define(IPermissionDefinitionContext context)
	{
		var myGroup = context.AddGroup(TenantServicePermissions.GroupName, L("Permission:TenantService"));
	}

	private static LocalizableString L(String name)
	{
		return LocalizableString.Create<TenantServiceResource>(name);
	}
}
