﻿using Library.Web.BusinessLogic.Repository.Abstract;
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

		if (!string.IsNullOrEmpty(author))
		{
			List<Guid> authors = await _dbContext.Authors
				.Where(a => a.Name.Contains(author))
				.Select(a => a.Id)
				.ToListAsync();

			books = books.Where(book => authors.Contains(book.AuthorId));
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


	/// <inheritdoc />
	public async Task<Book> GeBookAsync(string isbn)
	{
		return await _dbContext.Books
			.Where(book => book.ISBN == isbn)
			.Include(book => book.Author)
			.Include(book => book.BookInstances)
			.ThenInclude(bi => bi.Member)
			.FirstOrDefaultAsync();
	}
}