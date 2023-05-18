using Library.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Web;

public class LibraryContext : DbContext
{
	public LibraryContext(DbContextOptions<LibraryContext> options)
		: base(options)
	{
	}

	public DbSet<Author> Authors { get; set; }

	public DbSet<Book> Books { get; set; }

	public DbSet<BookInstance> BookInstances { get; set; }

	public DbSet<Member> Members { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Author>().HasKey(author => author.Id);
		modelBuilder.Entity<Author>().Property(author => author.Id).ValueGeneratedOnAdd();
		modelBuilder.Entity<Author>().Property(author => author.Name).IsRequired();

		modelBuilder.Entity<Book>().HasKey(book => book.ISBN);
		modelBuilder.Entity<Book>().Property(book => book.AuthorId).IsRequired();
		modelBuilder.Entity<Book>().Property(book => book.Title).IsRequired();
		modelBuilder.Entity<Book>().Property(book => book.Year).IsRequired();
		modelBuilder.Entity<Book>().Property(book => book.Genre).IsRequired();

		modelBuilder.Entity<BookInstance>().HasKey(bookInstance => bookInstance.Id);
		modelBuilder.Entity<BookInstance>().Property(book => book.Id).ValueGeneratedOnAdd();

		// Model Book to BookInstance one-to-many relationship.
		modelBuilder.Entity<Book>()
			.HasMany(book => book.BookInstances)
			.WithOne(bookInstance => bookInstance.Book)
			.HasForeignKey(bookInstance => bookInstance.ISBN)
			.IsRequired();
		
		// Model Book to BookInstance one-to-many relationship.
		modelBuilder.Entity<Author>()
			.HasMany(author => author.Books)
			.WithOne(book => book.Author)
			.HasForeignKey(book => book.AuthorId)
			.IsRequired();

		modelBuilder.Entity<Member>().HasKey(m => m.Id);
		modelBuilder.Entity<Member>().Property(m => m.Id).ValueGeneratedOnAdd();
		modelBuilder.Entity<Member>().Property(member => member.Name).IsRequired();
	}
}