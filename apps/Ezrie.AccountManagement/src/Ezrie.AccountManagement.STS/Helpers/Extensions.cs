
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Ezrie.AccountManagement.STS.ViewModels.Account;

namespace Ezrie.AccountManagement.STS.Helpers;

public static class Extensions
{
	/// <summary>
	/// Checks if the redirect URI is for a native client.
	/// </summary>
	/// <returns></returns>
	public static Boolean IsNativeClient(this AuthorizationRequest context)
	{
		return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal)
			   && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
	}

	public static IActionResult LoadingPage(this Controller controller, String viewName, String redirectUri)
	{
		controller.HttpContext.Response.StatusCode = 200;
		controller.HttpContext.Response.Headers["Location"] = "";

		return controller.View(viewName, new RedirectViewModel { RedirectUrl = redirectUri });
	}
}
