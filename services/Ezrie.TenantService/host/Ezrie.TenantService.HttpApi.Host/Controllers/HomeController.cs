using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Ezrie.TenantService.Controllers;

public class HomeController : AbpController
{
	public ActionResult Index() => Redirect("~/swagger/index.html");
}
