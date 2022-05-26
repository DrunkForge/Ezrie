using Microsoft.AspNetCore.Mvc;

namespace Ezrie.RelationshipManagement.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	[HttpGet]
	public IActionResult Index()
	{
		return View();
	}

	[HttpGet]
	public IActionResult Privacy()
	{
		return View();
	}

	[HttpGet]
	public IActionResult Error()
	{
		return View();
	}
}
