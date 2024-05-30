using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Models.ViewModels;

public class BooksPageViewModel : PageViewModel
{
	public BooksPageViewModel(
		string title,
		IReadOnlyList<Book> books) 
		: base(title)
	{
		Books = books;
	}
	
	public IReadOnlyList<Book> Books { get;  }

	public string TitleFilter { get; set; }
	
	public string AuthorFilter { get; set; }
	
	public Genre? GenreFilter { get; set; }
	
	public IReadOnlyList<SelectListItem> Genres { get; set; }
}