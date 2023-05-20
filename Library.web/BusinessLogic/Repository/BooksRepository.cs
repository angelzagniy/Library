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
		string title = null,
		string author = null,
		Genre genre = Genre.Any)
	{
		IQueryable<Book> books = _dbContext.Books;

		// Add filter by book title
		if (!string.IsNullOrEmpty(title))
		{
			books = books.Where(book => book.Title.Contains(title));
		}

		// Add filter by genre
		if (genre != Genre.Any)
		{
			books = books.Where(book => book.Genre == genre);
		}

		return await books
			.Include(book => book.BookInstances)
			.Include(book => book.Author)
			.ToListAsync();
	}

	/// <inheritdoc />
	public async Task AddBookAsync(
		Book book,
		int instancesCount)
	{
		await _dbContext.Books.AddAsync(book);

		for (int i = 0; i < instancesCount; i++)
		{
			await _dbContext.BookInstances.AddAsync(
				new BookInstance
				{
					ISBN = book.ISBN
				});
		}
		
		await _dbContext.SaveChangesAsync();
	}
}