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
    public async Task<IReadOnlyList<Member>> ListMembersAsync(
        string name = null,
        List<BookInstance> books = null)
    {
        IQueryable<Member> members = _dbContext.Members;

        return await members
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task AddMemberAsync(Member member)
    {
        await _dbContext.Members.AddAsync(member);
        await _dbContext.SaveChangesAsync();
    }
}