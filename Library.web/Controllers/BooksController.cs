using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Web.BusinessLogic.Abstract;
using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Controllers;

public class BooksController : Controller
{
    private readonly IBooksRepository _booksRepository;
    private readonly IAuthorsRepository _authorsRepository;
    private readonly IMembersRepository _membersRepository;
    private readonly IBooksVMBuilder _booksVmBuilder;

    public BooksController(
        IBooksRepository booksRepository,
        IAuthorsRepository authorsRepository,
        IMembersRepository membersRepository,
        IBooksVMBuilder booksVmBuilder)
    {
        _booksRepository = booksRepository;
        _authorsRepository = authorsRepository;
        _membersRepository = membersRepository;
        _booksVmBuilder = booksVmBuilder;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<BooksPageViewModel>> Index(
        string title,
        string author,
        Genre? genre)
    {

        BooksPageViewModel viewModel = await _booksVmBuilder.BuildBooksPageViewModelAsync(
            title: title,
            author: author,
            genre: genre ?? Genre.Any);
        
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
    public async Task<IActionResult> Get(string id)
    {
        Book book = await _booksRepository.GetBookAsync(id);

        return View(book);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Add()
    {
        IReadOnlyList<Author> authors = await _authorsRepository.ListAuthorsAsync();

        AddBookViewModel model = new()
        {
            Year = DateTime.Today.Year - 10,
            InstancesCount = 5,
            Authors = authors
                .Select(author => new SelectListItem(author.Name, author.Id.ToString()))
                .ToArray()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(AddBookViewModel newBook)
    {
        if (ModelState.IsValid)
        {
            await _booksRepository.AddBookAsync(
                new Book
                {
                    ISBN = newBook.ISBN,
                    Title = newBook.Title,
                    AuthorId = newBook.AuthorId!.Value,
                    Genre = newBook.Genre!.Value,
                    Year = newBook.Year
                },
                newBook.InstancesCount);

            return RedirectToAction(nameof(Index));
        }

        return View(newBook);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> ReturnBook(Guid id)
    {
        string isbn = await _booksRepository.ReleaseBookInstance(id);

        return !string.IsNullOrEmpty(isbn)
            ? RedirectToAction(nameof(Get), new { id = isbn })
            : RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> LendBook(Guid id)
    {
        LendBookViewModel viewModel = await _booksVmBuilder.BuildLendBookViewModelAsync(id);

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> LendBook(LendBookViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _booksRepository.LendBookAsync(model.BookInstance.Id, model.SelectedMemberId!.Value);

            return RedirectToAction(
                nameof(Get),
                new
                {
                    id = model.BookInstance.ISBN
                });
        }

        LendBookViewModel viewModel = await _booksVmBuilder.BuildLendBookViewModelAsync(model.BookInstance.Id);

        return View(viewModel);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Update(string id)
    {
        Book book = await _booksRepository.GetBookAsync(id);

        UpdateBookViewModel model = new()
        {
            ISBN = book.ISBN,
            Title = book.Title,
            Genre = book.Genre,
            Year = book.Year
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateBookViewModel patch)
    {
        if (ModelState.IsValid)
        {
            await _booksRepository.UpdateBookAsync(
                id: patch.ISBN,
                title: patch.Title,
                genre: patch.Genre!.Value,
                year: patch.Year);

            return RedirectToAction(nameof(Index));
        }

        return View(patch);
    }

    [HttpPost]
    public async Task<JsonResult> FindAuthors(string name)
    {
        IReadOnlyList<Author> authors = await _authorsRepository.FindAuthorsAsync(name, count: 10);

        var result = authors
            .Select(author =>
                new
                {
                    label = author.Name,
                    val = author.Id
                })
            .ToArray();

        return Json(result);
    }
    
    [HttpPost]
    public async Task<JsonResult> FindMembers(string name)
    {
        IReadOnlyList<Member> members = await _membersRepository.FindMembersAsync(name, count: 10);

        var result = members
            .Select(author =>
                new
                {
                    label = author.Name,
                    val = author.Id
                })
            .ToArray();

        return Json(result);
    }
}