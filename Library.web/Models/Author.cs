namespace Library.Web.Models;

public class Author
{
	public Guid Id { get; set; }

	public string Name { get; set; }

	/// <summary>
	/// Books written by the author.
	/// </summary>
	public ICollection<Book> Books { get; } = new List<Book>();
}