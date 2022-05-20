using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Ezrie.TenantService.Web.Menus;

public class TenantServiceMenuContributor : IMenuContributor
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
        context.Menu.AddItem(new ApplicationMenuItem(TenantServiceMenus.Prefix, displayName: "TenantService", "~/TenantService", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
