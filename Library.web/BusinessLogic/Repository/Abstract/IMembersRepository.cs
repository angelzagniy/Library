using Library.Web.Models;

namespace Library.Web.BusinessLogic.Repository.Abstract;


/// <summary>
/// Books repository
/// </summary>
public interface IMembersRepository
{
    /// <summary>
    /// Lists books.
    /// </summary>
    /// <param name="name">Optional name filter.</param>
    /// <returns>A collection of books matching filtering criteria, if specified.</returns>
    Task<IReadOnlyList<Member>> ListMembersAsync(string name = null);

    /// <summary>
    /// Adds member.
    /// </summary>
    Task AddMemberAsync(Member member);
    
    /// <summary>
    /// Gets member by ID.
    /// </summary>
    Task<Member> GetAsync(Guid id);
}