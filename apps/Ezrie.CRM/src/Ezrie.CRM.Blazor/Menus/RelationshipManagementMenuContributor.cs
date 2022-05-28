/*********************************************************************************************
* EzrieCRM
* Copyright (C) 2022 Doug Wilson (info@dougwilson.ca)
* 
* This program is free software: you can redistribute it and/or modify it under the terms of
* the GNU Affero General Public License as published by the Free Software Foundation, either
* version 3 of the License, or (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
* without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
* See the GNU Affero General Public License for more details.
* 
* You should have received a copy of the GNU Affero General Public License along with this
* program. If not, see <https://www.gnu.org/licenses/>.
*********************************************************************************************/

using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Ezrie.Configuration;
using Ezrie.AdministrationService.Localization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Ezrie.CRM.Localization;
using Volo.Abp.Account.Localization;

namespace Ezrie.CRM.Menus;

public class CrmMenuContributor : IMenuContributor
{
	private readonly IConfiguration _configuration;

	public CrmMenuContributor(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public async Task ConfigureMenuAsync(MenuConfigurationContext context)
	{
		if (context.Menu.Name == StandardMenus.Main)
		{
			await ConfigureMainMenuAsync(context);
		}
		else if (context.Menu.Name == StandardMenus.User)
		{
			await ConfigureUserMenuAsync(context);
		}
	}

	private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
	{
		var l = context.GetLocalizer<CrmResource>();

		context.Menu.Items.Insert(
			0,
			new ApplicationMenuItem(CrmMenus.Home.Top, l[CrmMenus.Home.Top], "/", icon: "fas fa-home")
		);

		context.Menu
			.AddItem(new ApplicationMenuItem(CrmMenus.Tenants.Top, l[CrmMenus.Tenants.Top], icon: "fa fa-book")
				.AddItem(new ApplicationMenuItem(CrmMenus.Tenants.List, l[CrmMenus.Tenants.List], url: "/tenants")
			)
		);

		return Task.CompletedTask;
	}

	private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
	{
		var accountStringLocalizer = context.GetLocalizer<AccountResource>();

		var identityServerUrl = _configuration["AuthServer:Authority"] ?? "";

		context.Menu.AddItem(new ApplicationMenuItem(
			"Account.Manage",
			accountStringLocalizer["MyAccount"],
			$"{identityServerUrl.EnsureEndsWith('/')}Manage?returnUrl={_configuration["App:SelfUrl"]}",
			icon: "fa fa-cog",
			order: 1000,
			null).RequireAuthenticated());

		return Task.CompletedTask;
	}
}
