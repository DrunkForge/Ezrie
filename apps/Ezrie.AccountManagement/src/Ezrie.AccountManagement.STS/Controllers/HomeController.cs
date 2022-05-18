
using IdentityServer4.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Skoruba.IdentityServer4.Shared.Configuration.Helpers;
using Ezrie.AccountManagement.STS.Helpers;
using Ezrie.AccountManagement.STS.ViewModels.Home;

namespace Ezrie.AccountManagement.STS.Controllers;

[SecurityHeaders]
public class HomeController : Controller
{
	private readonly IIdentityServerInteractionService _interaction;

	public HomeController(IIdentityServerInteractionService interaction)
	{
		_interaction = interaction;
	}

	public IActionResult Index()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult SetLanguage(String culture, String returnUrl)
	{
		Response.Cookies.Append(
			CookieRequestCultureProvider.DefaultCookieName,
			CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
			new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
		);
		return LocalRedirect(returnUrl);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult SelectTheme(String theme, String returnUrl)
	{
		Response.Cookies.Append(
			ThemeHelpers.CookieThemeKey,
			theme,
			new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
		);

		return LocalRedirect(returnUrl);
	}

	/// <summary>
	/// Shows the error page
	/// </summary>
	public async Task<IActionResult> Error(String errorId)
	{
		var vm = new ErrorViewModel();

		// retrieve error details from identityserver
		var message = await _interaction.GetErrorContextAsync(errorId);
		if (message != null)
		{
			vm.Error = message;
		}

		return View("Error", vm);
	}
}

