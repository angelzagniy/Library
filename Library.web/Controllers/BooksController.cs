using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Web.BusinessLogic.Abstract;
using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.BusinessLogic.Security;
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
	private readonly IBooksVMBuilder _booksVmBuilder;

	public BooksController(
		IBooksRepository booksRepository,
		IAuthorsRepository authorsRepository,
		IBooksVMBuilder booksVmBuilder)
	{
		_booksRepository = booksRepository;
		_authorsRepository = authorsRepository;
		_booksVmBuilder = booksVmBuilder;
	}

	[HttpGet]
	[Authorize]
	public async Task<ActionResult<BooksPageViewModel>> Index(
		string title,
		string author,
		Genre? genre)
	{
		IReadOnlyList<Book> books = await _booksRepository.ListBooksAsync(
			title: title,
			author: author,
			genre: genre ?? Genre.Any);

		BooksPageViewModel viewModel = new("Books", books)
		{
			TitleFilter = title,
			GenreFilter = genre,
			AuthorFilter = author
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
	public async Task<IActionResult> Get(string id)
	{
		Book book = await _booksRepository.GetBookAsync(id);

		return View(book);
	}

	[HttpGet]
	[Authorize(Roles = KnownRoles.Admin)]
	public async Task<IActionResult> Add()
	{
		IReadOnlyCollection<Author> authors = await _authorsRepository.ListAuthorsAsync();

		AddBookViewModel model = new()
		{
			Year = DateTime.Today.Year - 10,
			InstancesCount = 5,
			Authors = authors
				.Select(author => new SelectListItem(author.Name, author.Id.ToString()))
				.ToArray(),
			Genres = Enum.GetValues<Genre>()
				.Where(g => g != Genre.Any)
				.Select(genre => new SelectListItem(genre.ToString(), genre.ToString()))
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
	public async Task<IActionResult> Update(string isbn)
	{
		Book book = await _booksRepository.GetBookAsync(isbn);
		
		UpdateBookViewModel model = new()
		{
			ISBN = book.ISBN,
			Title = book.Title,
			AuthorId = book.AuthorId,
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
			await _booksRepository.UpdateBookAsync(patch.ISBN, patch.Title);
			return RedirectToAction(nameof(Index));
		}

		return View(patch);
	}
}