using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Web.Models;

namespace Library.Web.BusinessLogic.Repository.Abstract;

/// <summary>
/// Authors repository
/// </summary>
public interface IAuthorsRepository
{
	/// <summary>
	/// Lists authors.
	/// </summary>
	Task<IReadOnlyList<Author>> ListAuthorsAsync();

	Task AddAuthorAsync(Author author);

	Task<Author> GetAuthorAsync(Guid id);
	
	Task UpdateAuthorAsync(
		Guid id,
		string name);
}