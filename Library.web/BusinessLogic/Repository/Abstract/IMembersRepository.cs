using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    Task<Member> GetMemberAsync(Guid id);

    Task UpdateMemberAsync(
        Guid id,
        string name,
        string phoneNumber);

    /// <summary>
    /// Adds member.
    /// </summary>
    Task AddMemberAsync(Member member);

    /// <summary>
    /// Gets member by ID.
    /// </summary>
    Task<Member> GetAsync(Guid id);

    /// <summary>
    /// Find library members by name.
    /// </summary>
    /// <param name="name">Name pattern.</param>
    /// <param name="count">Max number of records to return.</param>
    Task<IReadOnlyList<Member>> FindMembersAsync(string name, int count);
}