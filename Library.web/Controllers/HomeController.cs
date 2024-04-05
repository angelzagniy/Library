using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;

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
		string name = null;
		string role = null;

		if (username == "admin" && password == "admin")
		{
			name = "Admin";
			role = "Admin";
		}
		else if (username == "jdoe" && password == "jdoe")
		{
			name = "John Doe";
			role = "User";
		}

		if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(role))
		{
			List<Claim> claims =
			[
				new Claim(type: ClaimTypes.Name, value: name),
				new Claim(type: ClaimTypes.Role, value: role)
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

	[HttpPost]
	public async Task<IActionResult> Logout()
	{
		await HttpContext.SignOutAsync();
		return RedirectToAction(nameof(Index));
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