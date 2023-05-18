namespace Library.Web.Models;

public class Book
{
	/// <summary>
	/// International Standard Book Number. Unique book identifier.
	/// </summary>
	public string ISBN { get; set; }

	/// <summary>
	/// Book title.
	/// </summary>
	public string Title { get; set; }

	/// <summary>
	/// Author
	/// </summary>
	public string Author { get; set; }

	/// <summary>
	/// Genre. 
	/// </summary>
	public Genre Genre { get; set; }

	/// <summary>
	/// Publish year. 
	/// </summary>
	public int Year { get; set; }
	
	/// <summary>
	/// Book instances (paper copies).
	/// </summary>
	public ICollection<BookInstance> BookInstances { get; } = new List<BookInstance>(); 
}