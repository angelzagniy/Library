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
	public async Task<IReadOnlyList<User>> ListUsersAsync(string name = null)
	{
		IQueryable<User> users = _dbContext.Users;

		if (!string.IsNullOrEmpty(name))
		{
			users = users.Where(user => user.Name.Contains(name));
		}

		return await users.ToListAsync();
	}

	/// <inheritdoc />
	public async Task<User> GetUserAsync(Guid id)
	{
		return await _dbContext.Users.FindAsync(id);
	}

	/// <inheritdoc />
	public async Task AddUserAsync(User user)
	{
		await _dbContext.Users.AddAsync(user);
		await _dbContext.SaveChangesAsync();
	}

	/// <inheritdoc />
	public async Task DeleteUserAsync(User user)
	{
		_dbContext.Users.Remove(user);
		await _dbContext.SaveChangesAsync();
	}
}