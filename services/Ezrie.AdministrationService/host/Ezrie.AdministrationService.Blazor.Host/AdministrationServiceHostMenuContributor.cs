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

namespace Ezrie.AdministrationService;

public class AdministrationServiceHostMenuContributor : IMenuContributor
{
	private readonly IConfiguration _configuration;

	public AdministrationServiceHostMenuContributor(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public async Task ConfigureMenuAsync(MenuConfigurationContext context)
	{
		if (context.Menu.Name == StandardMenus.User)
		{
			await ConfigureUserMenuAsync(context);
		}
	}

	private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
	{
		var L = context.GetLocalizer<AdministrationServiceResource>();

		var hostOptions = _configuration.GetHostOptions();

		context.Menu.AddItem(new ApplicationMenuItem(
			"Account.Manage",
			L["Menu:ManageYourProfile"],
			$"{hostOptions.Authority.EnsureEndsWith('/')}Manage?returnUrl={hostOptions.BaseUrl}",
			icon: "fa fa-cog",
			order: 1000,
			null).RequireAuthenticated());

		return Task.CompletedTask;
	}
}
