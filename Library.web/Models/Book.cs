using System;
using System.Collections.Generic;

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
	public Guid AuthorId { get; set; }

	/// <summary>
	/// Genre. 
	/// </summary>
	public Genre Genre { get; set; }

	/// <summary>
	/// Publish year. 
	/// </summary>
	public int Year { get; set; }

	/// <summary>
	/// Reference to the book's author.
	/// </summary>
	public Author Author { get; set; } = null!;
	
	/// <summary>
	/// Book instances (paper copies) owned by the library.
	/// </summary>
	public ICollection<BookInstance> BookInstances { get; } = new List<BookInstance>(); 
}