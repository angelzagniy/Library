using Library.Web.BusinessLogic.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;

public class UsersController : Controller
{
	[HttpGet]
	[Authorize(Roles = KnownRoles.Admin)]
	public IActionResult Index()
	{
		return View();
	}
}