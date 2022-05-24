using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ezrie.RelationshipManagement.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;

namespace Ezrie.RelationshipManagement.Blazor.Menus;

public class RelationshipManagementMenuContributor : IMenuContributor
{
	private readonly IConfiguration _configuration;

	public RelationshipManagementMenuContributor(IConfiguration configuration)
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
		var l = context.GetLocalizer<RelationshipManagementResource>();

		context.Menu.Items.Insert(
			0,
			new ApplicationMenuItem(
				RelationshipManagementMenus.Home,
				l["Menu:Home"],
				"/",
				icon: "fas fa-home"
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
