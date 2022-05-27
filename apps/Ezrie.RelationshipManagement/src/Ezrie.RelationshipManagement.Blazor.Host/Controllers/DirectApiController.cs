using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ezrie.RelationshipManagement.Controllers;

[ValidateAntiForgeryToken]
[Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class DirectApiController : ControllerBase
{
	[HttpGet]
	public IEnumerable<String> Get()
	{
		return new List<String> { "some data", "more data", "loads of data" };
	}
}
