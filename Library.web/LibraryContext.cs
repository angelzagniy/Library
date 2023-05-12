using Library.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Web;

public class LibraryContext : DbContext
{
	public LibraryContext(DbContextOptions<LibraryContext> options)
		: base(options)
	{
	}

	public DbSet<Book> Books { get; set; }

	public DbSet<Member> Members { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Book>().HasKey(book => book.Id);
		modelBuilder.Entity<Book>().Property(book => book.Id).ValueGeneratedOnAdd();

		modelBuilder.Entity<Member>().HasKey(m => m.Id);
		modelBuilder.Entity<Member>().Property(m => m.Id).ValueGeneratedOnAdd();
	}
}