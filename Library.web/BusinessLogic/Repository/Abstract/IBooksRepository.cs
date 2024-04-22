using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Web.Models;

namespace Library.Web.BusinessLogic.Repository.Abstract;

/// <summary>
/// Books repository
/// </summary>
public interface IBooksRepository
{
	/// <summary>
	/// Lists books.
	/// </summary>
	/// <param name="title">Optional book title filter.</param>
	/// <param name="author">Optional author filter.</param>
	/// <param name="genre">Optional genre filter.</param>
	/// <returns>A collection of books matching filtering criteria, if specified.</returns>
	Task<IReadOnlyList<Book>> ListBooksAsync(
		string title = null,
		string author = null,
		Genre genre = Genre.Any);

	/// <summary>
	/// Adds book to the repository.
	/// </summary>
	Task AddBookAsync(Book book, int instancesCount);
	
	/// <summary>
	/// Gets book by ISBN.
	/// </summary>
	Task <Book> GetBookAsync(string isbn);

	/// <summary>
	/// Releases book instance (returns a book) ID.
	/// </summary>
	Task<string> ReleaseBookInstance(Guid instanceId);

	/// <summary>
	/// Retrieves a book instance.
	/// </summary>
	Task<BookInstance> GetBookInstanceAsync(Guid id);

	/// <summary>
	/// Assigns book instance to a member (lends a book).
	/// </summary>
	Task<bool> LendBookAsync(Guid bookInstanceId, Guid memberId);
	
	/// <summary>
	/// Update book in the repository.
	/// </summary>
	/// <param name="id">Book ID (ISBN).</param>
	/// <param name="title">Updated title.</param>
	/// <param name="genre">update genre.</param>
	/// <param name="year">Updated year.</param>
	Task UpdateBookAsync(
		string id,
		string title,
		Genre genre,
		int year);
}