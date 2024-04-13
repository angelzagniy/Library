using System;
using System.Collections.Generic;
using System.Linq;
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

		UserPageViewModel viewModel = new("Users", users)
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
			// Check that user name is unique
			IReadOnlyList<User> users = await _usersRepository.ListUsersAsync(newUser.Name);
			if (users.Any(user => string.Equals(
				    user.Username,
				    newUser.UserName,
				    StringComparison.OrdinalIgnoreCase)))
			{
				ModelState.AddModelError(
					key: nameof(newUser.UserName),
					errorMessage: "There is already a user with such username.");

				return View(newUser);
			}

			// Check that passwords match.
			if (newUser.Password != newUser.RepeatPassword)
			{
				ModelState.AddModelError(
					key: nameof(newUser.RepeatPassword),
					errorMessage: "Passwords do not match.");

				return View(newUser);
			}

			// Generate password and salt - these should be from a single HashPassword call!
			(string HashedPassword, string Salt) secret = CryptoUtilities.HashPassword(newUser.Password);

			await _usersRepository.AddUserAsync(
				new User
				{
					Name = newUser.Name,
					Username = newUser.UserName,
					Role = newUser.Role,
					Password = secret.HashedPassword,
					Salt = secret.Salt
				}
			);

			return RedirectToAction(nameof(Index));
		}

		return View(newUser);
	}

	[HttpGet]
	[Authorize(Roles = KnownRoles.Admin)]
	public async Task<IActionResult> Delete(Guid id)
	{
		User user = await _usersRepository.GetUserAsync(id);
		if (user is { IsBuiltIn: false })
		{
			await _usersRepository.DeleteUserAsync(user);
		}

		return RedirectToAction(nameof(Index));
	}
}