﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
	
	public async Task<Author> GetAuthorAsync(Guid id)
	{
		return await _dbContext.Authors.FindAsync(id);
	}

	public async Task UpdateAuthorAsync(Guid id, string name)
	{
		Author author = await GetAuthorAsync(id);

		author.Name = name;

		await _dbContext.SaveChangesAsync();
	}
}