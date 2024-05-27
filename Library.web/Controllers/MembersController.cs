using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
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

	[Authorize]
	public async Task<ActionResult<MembersPageViewModel>> Index(string name)
	{
		IReadOnlyList<Member> members = await _membersRepository.ListMembersAsync(name: name);

		MembersPageViewModel viewModel = new("Members", members)
		{
			NameFilter = name
		};

		return View(viewModel);
	}
	
	[HttpGet]
	[Authorize]
	public IActionResult Reset()
	{
		return RedirectToAction(nameof(Index));
	}

	[HttpGet]
	[Authorize]
	public IActionResult Add()
	{
		AddMemberViewModel model = new()
		{
			PhoneNumber = "+38"
		};

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
					Name = newMember.Name,
					PhoneNumber = newMember.PhoneNumber
				});

			return RedirectToAction(nameof(Index));
		}

		return View(newMember);
	}
	
	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Update(Guid id)
	{
		Member member = await _membersRepository.GetMemberAsync(id);
		
		UpdateMemberViewModel model = new()
		{
			Id = member.Id,
			Name = member.Name,
			PhoneNumber = member.PhoneNumber
		};

		return View(model);
	}
    
	[HttpPost]
	public async Task<IActionResult> Update(UpdateMemberViewModel patch)
	{
		if (ModelState.IsValid)
		{
			await _membersRepository.UpdateMemberAsync(patch.Id, patch.Name, patch.PhoneNumber);
			return RedirectToAction(nameof(Index));
		}

		return View(patch);
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> Get(Guid id)
	{
		Member member = await _membersRepository.GetAsync(id);

		return View(member);
	}

	[HttpGet]
	[Authorize]
	public async Task<IActionResult> ReturnBook(Guid id)
	{
		await _booksRepository.ReleaseBookInstance(id);

		return RedirectToAction(nameof(Index));
	}
}