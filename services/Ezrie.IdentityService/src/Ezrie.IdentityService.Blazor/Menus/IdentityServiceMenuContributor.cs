using Volo.Abp.UI.Navigation;

namespace Ezrie.IdentityService.Menus;

public class IdentityServiceMenuContributor : IMenuContributor
{
	public async Task ConfigureMenuAsync(MenuConfigurationContext context)
	{
		if (context.Menu.Name == StandardMenus.Main)
		{
			await ConfigureMainMenuAsync(context);
		}
	}

	private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
	{
		//Add main menu items.
		context.Menu.AddItem(new ApplicationMenuItem(IdentityServiceMenus.Prefix, displayName: "IdentityService", "/IdentityService", icon: "fa fa-globe"));

		return Task.CompletedTask;
	}
}
