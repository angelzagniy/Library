using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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
		HomePageViewModel viewModel = new(title: "Home");

		return View(viewModel);
	}

	[HttpGet]
	public IActionResult Login()
	{
		LoginViewModel viewModel = new();

		return View(viewModel);
	}

	[HttpPost]
	public async Task<IActionResult> Login(
		string username,
		string password,
		string returnUrl)
	{
		if (username == "Admin" && password == "Admin")
		{
			List<Claim> claims =
			[
				new Claim(type: ClaimTypes.Name, value: username),
				new Claim(type: ClaimTypes.Role, value: "Admin")
			];

			ClaimsIdentity claimsIdentity = new(claims, authenticationType: "Login");

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity));

			return string.IsNullOrEmpty(returnUrl)
				? RedirectToAction("Index") 
				: Redirect(returnUrl);
		}

		return View(new LoginViewModel());
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