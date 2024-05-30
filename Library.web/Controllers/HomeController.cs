using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.BusinessLogic.Security;
using Library.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Library.Web.Controllers;

public class HomeController : Controller
{
    private readonly IUserRepository _userRepository;

    public HomeController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public ActionResult<HomePageViewModel> Index()
    {
        HomePageViewModel viewModel = new(title: "Home");

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult Login()
    {
        LoginViewModel pageViewModel = new();

        return View(pageViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        User user = await _userRepository.FindUserAsync(model.Username);

        if (user == null)
        {
            ModelState.AddModelError(
                key: nameof(model.Username),
                errorMessage: Resources.Shared.UnknownUserError);

            return View(model);
        }

        if (!CryptoUtilities.IsPasswordValid(model.Password, user.Password, user.Salt))
        {
            ModelState.AddModelError(
                key: nameof(model.Password),
                errorMessage: Resources.Shared.AuthenticationFailedError);

            return View(model);
        }

        // if (username == "admin" && password == "admin")
        // {
        // 	name = "Admin";
        // 	role = "Admin";
        // }
        // else if (username == "jdoe" && password == "jdoe")
        // {
        // 	name = "John Doe";
        // 	role = "User";
        // }

        List<Claim> claims =
        [
            new Claim(type: ClaimTypes.Name, user.Name),
            new Claim(type: ClaimTypes.Role, user.Role)
        ];

        ClaimsIdentity claimsIdentity = new(claims, authenticationType: "Login");

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        return string.IsNullOrEmpty(model.ReturnUrl)
            ? RedirectToAction("Index")
            : Redirect(model.ReturnUrl);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        Response.Cookies.Append(
            key: CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1)
            }
        );

        return LocalRedirect(returnUrl);
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return View();
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