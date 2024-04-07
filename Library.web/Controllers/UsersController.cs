using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.BusinessLogic.Security;
using Library.Web.Models;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;

public class UsersController : Controller
{
	private readonly IUserRepository _usersRepository;

	public UsersController(IUserRepository usersRepository)
	{
		_usersRepository = usersRepository;
	}
	
	[HttpGet]
	[Authorize(Roles = KnownRoles.Admin)]
	public async Task<ActionResult<UserPageViewModel>> Index(string name)
	{
		IReadOnlyList<User> users = await _usersRepository.ListUsersAsync(name);
		
		UserPageViewModel viewModel = new ("Users", users)
		{
			NameFilter = name
		};
		
		return View(viewModel);
	}
	
	[HttpGet]
	public IActionResult Reset()
	{
		return RedirectToAction(nameof(Index));
	}
	
	[HttpGet]
	[Authorize(Roles = "Admin")]
	public IActionResult Add()
	{
		AddUserViewModel model = new();

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Add(AddUserViewModel newUser)
	{
		if (ModelState.IsValid)
		{
			await _usersRepository.AddUserAsync(
				new User
				{
					Name = newUser.Name,
					Username = newUser.UserName,
					Role = newUser.Role,
					Password = CryptoUtilities.HashPassword(newUser.Password).HashedPassword,
					Salt = CryptoUtilities.HashPassword(newUser.Password).Salt,
				});

			return RedirectToAction(nameof(Index));
		}

		return View(newUser);
	}
}