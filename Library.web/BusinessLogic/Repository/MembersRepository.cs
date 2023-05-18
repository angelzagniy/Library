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
        string surname = null,
        List<BookInstance> books = null)
    {
        return await _dbContext.Members.ToListAsync();
    }
}