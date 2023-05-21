using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.BusinessLogic.Repository;

/// <inheritdoc />
internal class AuthorsRepository : IAuthorsRepository
{
	private readonly LibraryContext _dbContext;

	public AuthorsRepository(LibraryContext dbContext)
	{
		_dbContext = dbContext;
	}

	/// <inheritdoc />
	public async Task<IReadOnlyList<Author>> ListAuthorsAsync()
	{
		return await _dbContext.Authors.ToListAsync();
	}
	
	/// <inheritdoc />
	public async Task AddAuthorAsync(Author author)
	{
		await _dbContext.Authors.AddAsync(author);
		await _dbContext.SaveChangesAsync();
	}
}