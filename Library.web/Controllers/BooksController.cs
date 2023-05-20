using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers;

public class BooksController : Controller
{
	private readonly IBooksRepository _booksRepository;

	public BooksController(IBooksRepository booksRepository)
	{
		_booksRepository = booksRepository;
	}

	public async Task<ActionResult<BooksPageViewModel>> Index(
		string title)
	{
		IReadOnlyList<Book> books = await _booksRepository.ListBooksAsync(
			title);
		
		BooksPageViewModel viewModel = new (
			"Books",
			books);

		return View(viewModel);
	}
}