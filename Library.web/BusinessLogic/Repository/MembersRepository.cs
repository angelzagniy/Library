using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.BusinessLogic.Repository;

/// <inheritdoc />
public class MembersRepository : IMembersRepository
{
	private readonly LibraryContext _dbContext;

	public MembersRepository(LibraryContext dbContext)
	{
		_dbContext = dbContext;
	}

	/// <inheritdoc />
	public async Task<IReadOnlyList<Member>> ListMembersAsync(string name = null)
	{
		IQueryable<Member> members = _dbContext.Members;

		// Add filter by member name
		if (!string.IsNullOrEmpty(name))
		{
			members = members.Where(member => member.Name.Contains(name));
		}

		return await members
			.ToListAsync();
	}

	/// <inheritdoc />
	public async Task AddMemberAsync(Member member)
	{
		await _dbContext.Members.AddAsync(member);
		await _dbContext.SaveChangesAsync();
	}

	/// <inheritdoc />
	public async Task<Member> GetAsync(Guid id)
	{
		return await _dbContext.Members
			.Where(member => member.Id == id)
			.Include(member => member.BookInstances)
			.ThenInclude(bookInstance => bookInstance.Book)
			.ThenInclude(book => book.Author)
			.FirstOrDefaultAsync();
	}
}