using Microsoft.AspNetCore.Mvc;
using Ezrie.AccountManagement.STS.Configuration.Interfaces;

namespace Ezrie.AccountManagement.STS.ViewComponents;

public class IdentityServerAdminLinkViewComponent : ViewComponent
{
	private readonly IRootConfiguration _configuration;

	public IdentityServerAdminLinkViewComponent(IRootConfiguration configuration)
	{
		_configuration = configuration;
	}

	public IViewComponentResult Invoke()
	{
		var identityAdminUrl = _configuration.AdminConfiguration.IdentityAdminBaseUrl;

		return View(model: identityAdminUrl);
	}
}

