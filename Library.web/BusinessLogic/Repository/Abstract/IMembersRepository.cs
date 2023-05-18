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
    /// <param name="surname">Optional surname filter.</param>
    /// <param name="books">Optional list of taken books.</param>
    /// <returns>A collection of books matching filtering criteria, if specified.</returns>
    Task<IReadOnlyList<Member>> ListMembersAsync(
        string name = null,
        string surname = null,
        List<BookInstance> books = null);
}