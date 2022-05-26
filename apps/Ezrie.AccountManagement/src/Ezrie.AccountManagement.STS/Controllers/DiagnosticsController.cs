
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ezrie.AccountManagement.STS.Helpers;
using Ezrie.AccountManagement.STS.ViewModels.Diagnostics;

namespace Ezrie.AccountManagement.STS.Controllers;

[SecurityHeaders]
[Authorize]
public class DiagnosticsController : Controller
{
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		var localAddresses = new String[] { "127.0.0.1", "::1", HttpContext.Connection?.LocalIpAddress?.ToString() ?? String.Empty };
		if (!localAddresses.Contains(HttpContext.Connection?.RemoteIpAddress?.ToString()))
		{
			return NotFound();
		}

		var model = new DiagnosticsViewModel(await HttpContext.AuthenticateAsync());
		return View(model);
	}
}

