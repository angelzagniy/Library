using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;

public class MembersController : Controller
{
	private readonly IMembersRepository _membersRepository;
	private readonly IBooksRepository _booksRepository;

	public MembersController(
		IMembersRepository membersRepository,
		IBooksRepository booksRepository)
	{
		_membersRepository = membersRepository;
		_booksRepository = booksRepository;
	}

	public async Task<ActionResult<MembersPageViewModel>> Index()
	{
		IReadOnlyList<Member> members = await _membersRepository.ListMembersAsync();

		MembersPageViewModel viewModel = new(
			"Members",
			members);

		return View(viewModel);
	}

	[HttpGet]
	public IActionResult Add()
	{
		AddMemberViewModel model = new();

		return View(model);
	}

	[HttpPost]
	public async Task<IActionResult> Add(AddMemberViewModel newMember)
	{
		if (ModelState.IsValid)
		{
			await _membersRepository.AddMemberAsync(
				new Member
				{
					Name = newMember.Name
				});

			return RedirectToAction(nameof(Index));
		}

		return View(newMember);
	}

	[HttpGet]
	public async Task<IActionResult> Get(Guid id)
	{
		Member member = await _membersRepository.GetAsync(id);

		return View(member);
	}

	[HttpGet]
	public async Task<IActionResult> ReturnBook(Guid id)
	{
		await _booksRepository.ReleaseBookInstance(id);

		return RedirectToAction(nameof(Index));
	}
}