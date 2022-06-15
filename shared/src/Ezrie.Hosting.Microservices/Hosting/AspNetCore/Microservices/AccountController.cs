using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace Ezrie.Hosting.AspNetCore.Microservices;

[Authorize]
public class AccountController : Controller
{
	private readonly ILogger<AccountController> _logger;
	private readonly UserManager<Volo.Abp.Identity.IdentityUser> _userManager;
	private readonly SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> _signInManager;

	public AccountController(
		ILogger<AccountController> logger, 
		UserManager<Volo.Abp.Identity.IdentityUser> userManager, 
		SignInManager<Microsoft.AspNetCore.Identity.IdentityUser> signInManager)
	{
		_logger = logger;
		_userManager = userManager;
		_signInManager = signInManager;
	}

	//
	// POST: /Account/ExternalLogin
	[HttpPost]
	[AllowAnonymous]
	[ValidateAntiForgeryToken]
	public IActionResult ExternalLogin(String provider, String? returnUrl = null)
	{
		// Request a redirect to the external login provider.
		var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
		var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
		return Challenge(properties, provider);
	}

	//
	// GET: /Account/ExternalLoginCallback
	[HttpGet]
	[AllowAnonymous]
	public async Task<IActionResult> ExternalLoginCallback(String? returnUrl = null, String? remoteError = null)
	{
		if (returnUrl == null)
		{
			return BadRequest("returnUrl is required.");
		}

		if (remoteError != null)
		{
			return BadRequest(remoteError);
		}

		var info = await _signInManager.GetExternalLoginInfoAsync();
		if (info == null)
		{
			return Forbid();
		}

		// Sign in the user with this external login provider if the user already has a login.
		var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
		if (result.Succeeded)
		{
			// Update any authentication tokens if login succeeded
			await _signInManager.UpdateExternalAuthenticationTokensAsync(info);

			_logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);

			return Redirect(returnUrl);
		}

		return Unauthorized();
	}
}
