using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Library.Web.Models.ViewModels;

namespace Library.Web.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;

	public HomeController(
		ILogger<HomeController> logger)
	{
		_logger = logger;
	}

	public ActionResult<HomePageViewModel> Index()
	{
		HomePageViewModel viewModel = new ("Home");

		return View(viewModel);
	}


	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(
			new ErrorViewModel
			{
				RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
			});
	}
}