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
	public async Task<IActionResult> Get(string id)
	{
		Book book = await _booksRepository.GeBookAsync(id);

		return View(book);
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
	public async Task<IActionResult> Release(Guid id)
	{
		string isbn = await _booksRepository.ReleaseBookInstance(id);

		return !string.IsNullOrEmpty(isbn)
			? RedirectToAction(nameof(Get), new { id = isbn })
			: RedirectToAction(nameof(Index));
	}
}