using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;

public class AuthorsController: Controller
{
    private readonly IAuthorsRepository _authorsRepository;

    public AuthorsController(IAuthorsRepository authorsRepository)
    {
        _authorsRepository = authorsRepository;
    }
    
    public async Task<ActionResult<MembersPageViewModel>> Index()
    {
        IReadOnlyList<Author> authors = await _authorsRepository.ListAuthorsAsync();
		
        AuthorPageViewModel viewModel = new (
            "Authors",
            authors);

        return View(viewModel);
    }
    
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Add()
    {
        AddAuthorViewModel model = new();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddAuthorViewModel newAuthor)
    {
        if (ModelState.IsValid)
        {
            await _authorsRepository.AddAuthorAsync(
                new Author
                {
                    Name = newAuthor.Name
                });

            return RedirectToAction(nameof(Index));
        }

        return View(newAuthor);
    }
}