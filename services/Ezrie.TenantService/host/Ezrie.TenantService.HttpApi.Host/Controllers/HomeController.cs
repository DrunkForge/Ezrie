using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.DependencyInjection;

namespace Ezrie.TenantService.Controllers;

[RemoteService(Name = TenantServiceRemoteServiceConsts.RemoteServiceName)]
[ControllerName("Home")]
[Route("/")]
[ExposeServices(typeof(HomeController))]
[ApiController]
public class HomeController : AbpController, IApplicationService, IRemoteService
{
	[HttpGet]
	public ActionResult Index() => Redirect("~/swagger/index.html");
}
