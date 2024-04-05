using System;

namespace Library.Web.Models;

/// <summary>
/// Model for library member.
/// </summary>
public class User
{
	/// <summary>
	/// Member unique ID.
	/// </summary>
	public Guid Id { get; set; }

	/// <summary>
	/// User full name.
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// User role.
	/// </summary>
	public string Role { get; set; }

	/// <summary>
	/// User login name.
	/// </summary>
	public string Username { get; set; }

	/// <summary>
	/// Hashed password.
	/// </summary>
	public string Password { get; set; }

	/// <summary>
	/// Password "salt".
	/// </summary>
	public string Salt { get; set; }
}