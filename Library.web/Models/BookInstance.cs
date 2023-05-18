namespace Library.Web.Models;

/// <summary>
/// Domain model for a instance (a concrete paper copy). 
/// </summary>
public class BookInstance
{
	/// <summary>
	/// Unique book ID.
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// Book ID (ISBN). Required foreign key.
	/// </summary>
	public string BookId { get; set; }

	/// <summary>
	/// Reference navigation to book (principal in one-to-many relationship).
	/// </summary>
	public Book Book { get; set; } = null!;
}