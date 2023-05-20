using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Controllers;

public class BooksController : Controller
{
	private readonly IBooksRepository _booksRepository;
	private readonly IAuthorsRepository _authorsRepository;

	public BooksController(
		IBooksRepository booksRepository,
		IAuthorsRepository authorsRepository)
	{
		_booksRepository = booksRepository;
		_authorsRepository = authorsRepository;
	}

	[HttpGet]
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
	public async Task<IActionResult> Add()
	{
		IReadOnlyCollection<Author> authors = await _authorsRepository.ListAuthorsAsync();

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
	public async Task<IActionResult> Add(AddBookViewModel newBook)
	{
		await _booksRepository.AddBookAsync(
			new Book
			{
				ISBN = newBook.ISBN,
				Title = newBook.Title,
				AuthorId = newBook.AuthorId,
				Genre = newBook.Genre,
				Year = newBook.Year
			},
			newBook.InstancesCount);

		return RedirectToAction("Index");
	}
}