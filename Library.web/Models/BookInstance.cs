using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Models;

/// <summary>
/// Domain model for a book instance (a concrete paper copy). 
/// </summary>
[PrimaryKey(nameof(Id))]
public class BookInstance
{
	/// <summary>
	/// Unique book ID.
	/// </summary>
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }

	/// <summary>
	/// Book ID (ISBN). Required foreign key.
	/// </summary>
	public string ISBN { get; set; }

	/// <summary>
	/// Optional reference to library member.
	/// If set, the instance is taken by the specified member.
	/// </summary>
	public Guid? MemberId { get; set; }

	/// <summary>
	/// Reference navigation to book (principal in one-to-many relationship).
	/// </summary>
	public Book Book { get; set; } = null!;

	public Member Member { get; set; }
}