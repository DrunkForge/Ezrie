using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ezrie.RelationshipManagement.Controllers;

// orig src https://github.com/berhir/BlazorWebAssemblyCookieAuth
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
	[HttpGet("Login")]
	public ActionResult Login(String returnUrl)
	{
		return Challenge(new AuthenticationProperties
		{
			RedirectUri = !String.IsNullOrEmpty(returnUrl) ? returnUrl : "/"
		});
	}

	[ValidateAntiForgeryToken]
	[Authorize]
	[HttpPost("Logout")]
	public IActionResult Logout()
	{
		return SignOut(
			new AuthenticationProperties { RedirectUri = "/" },
			CookieAuthenticationDefaults.AuthenticationScheme,
			OpenIdConnectDefaults.AuthenticationScheme);
	}
}
