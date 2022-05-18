using Volo.Abp.UI.Navigation;

namespace Ezrie.AdministrationService.Blazor.Menus;

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
