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
}