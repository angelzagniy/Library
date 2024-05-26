using System;
using System.Collections.Generic;

namespace Library.Web.Models;

/// <summary>
/// Model for library member.
/// </summary>
public class Member
{
    /// <summary>
    /// Member unique ID.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Member full name.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Member phone number.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// A collection of the books being taken by the member.
    /// </summary>
    public ICollection<BookInstance> BookInstances { get; } = new List<BookInstance>(); 
}