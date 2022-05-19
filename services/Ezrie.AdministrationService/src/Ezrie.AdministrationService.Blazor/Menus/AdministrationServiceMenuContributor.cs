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

using Volo.Abp.UI.Navigation;

namespace Ezrie.AdministrationService.Menus;

public class AdministrationServiceMenuContributor : IMenuContributor
{
	public async Task ConfigureMenuAsync(MenuConfigurationContext context)
	{
		if (context.Menu.Name == StandardMenus.Main)
		{
			await ConfigureMainMenuAsync(context);
		}
	}

	private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
	{
		//Add main menu items.
		context.Menu.AddItem(new ApplicationMenuItem(AdministrationServiceMenus.Prefix, displayName: "AdministrationService", "/AdministrationService", icon: "fa fa-globe"));

		return Task.CompletedTask;
	}
}
