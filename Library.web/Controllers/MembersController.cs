using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;

public class MembersController : Controller
{
    private readonly IMembersRepository _membersRepository;

    public MembersController(IMembersRepository membersRepository)
    {
        _membersRepository = membersRepository;
    }
    
    public async Task<ActionResult<MembersPageViewModel>> Index()
    {
        IReadOnlyList<Member> members = await _membersRepository.ListMembersAsync();
		
        MembersPageViewModel viewModel = new (
            "Members",
            members);

        return View(viewModel);
    }
}