using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.BusinessLogic.Repository;

/// <inheritdoc />
internal class BooksRepository : IBooksRepository
{
	private readonly LibraryContext _dbContext;

	public BooksRepository(LibraryContext dbContext)
	{
		_dbContext = dbContext;
	}

	/// <inheritdoc />
	public async Task<IReadOnlyList<Book>> ListBooksAsync(
		string name = null,
		string author = null,
		Genre genre = Genre.Any)
	{
		return await _dbContext.Books.ToListAsync();
	}
}