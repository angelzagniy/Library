﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Web.Models;

namespace Library.Web.BusinessLogic.Repository.Abstract;

public interface IUserRepository
{
	/// <summary>
	/// Finds user by username (login name).
	/// </summary>
	Task<User> FindUserAsync(string username);

	/// <summary>
	/// Fetches all users from the database.
	/// </summary>
	Task<IReadOnlyList<User>> ListUsersAsync(string name);
	
	/// <summary>
	/// Gets user by ID.
	/// </summary>
	Task<User> GetUserAsync(Guid id);
	
	/// <summary>
	/// Adds user to the database.
	/// </summary>
	Task AddUserAsync(User user);

	/// <summary>
	/// Updates user in the database.
	/// </summary>
	Task UpdateUserAsync(
		Guid id,
		string name,
		string role);

	/// <summary>
	/// Deletes a user from the database.
	/// </summary>
	Task DeleteUserAsync(User user);
}