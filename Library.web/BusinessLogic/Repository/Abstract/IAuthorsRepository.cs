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
	Task<IReadOnlyList<Author>> ListAuthorsAsync(string name = null);

	/// <summary>
	/// Finds author by name.
	/// </summary>
	/// <param name="name">Author name pattern.</param>
	/// <param name="count">Max number of records to return.</param>
	Task<IReadOnlyList<Author>> FindAuthorsAsync(string name, int count);

	Task AddAuthorAsync(Author author);

	Task<Author> GetAuthorAsync(Guid id);
	
	Task UpdateAuthorAsync(
		Guid id,
		string name);
}