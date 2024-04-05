using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.BusinessLogic.Repository;

public class UserRepository : IUserRepository
{
	private readonly LibraryContext _dbContext;

	public UserRepository(LibraryContext dbContext)
	{
		_dbContext = dbContext;
	}

	/// <inheritdoc />
	public Task<User> FindUserAsync(string username)
	{
		return _dbContext.Users
			.Where(user => user.Username == username)
			.FirstOrDefaultAsync();
	}

	/// <inheritdoc />
	public Task<IReadOnlyList<User>> ListUsersAsync()
	{
		throw new NotImplementedException();
	}

	/// <inheritdoc />
	public Task<User> GetUserAsync(Guid id)
	{
		throw new NotImplementedException();
	}
}