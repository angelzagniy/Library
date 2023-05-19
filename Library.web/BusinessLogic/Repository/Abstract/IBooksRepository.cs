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
}