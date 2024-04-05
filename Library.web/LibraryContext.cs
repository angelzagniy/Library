using Library.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Web;

public class LibraryContext : DbContext
{
	public LibraryContext(DbContextOptions<LibraryContext> options)
		: base(options)
	{
	}

	public DbSet<User> Users { get; set; }

	public DbSet<Author> Authors { get; set; }

	public DbSet<Member> Members { get; set; }

	public DbSet<Book> Books { get; set; }

	public DbSet<BookInstance> BookInstances { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Author>().HasKey(author => author.Id);
		modelBuilder.Entity<Author>().Property(author => author.Id).ValueGeneratedOnAdd();
		modelBuilder.Entity<Author>().Property(author => author.Name).IsRequired().HasMaxLength(100);

		modelBuilder.Entity<Book>().HasKey(book => book.ISBN);
		modelBuilder.Entity<Book>().Property(book => book.ISBN)
			.IsRequired()
			.HasMaxLength(17);
		
		modelBuilder.Entity<Book>().Property(book => book.AuthorId).IsRequired();
		
		modelBuilder.Entity<Book>().Property(book => book.Title)
			.IsRequired()
			.HasMaxLength(200);
		modelBuilder.Entity<Book>().Property(book => book.Year).IsRequired();
		modelBuilder.Entity<Book>().Property(book => book.Genre)
			.IsRequired()
			.HasConversion<string>();

		modelBuilder.Entity<BookInstance>().HasKey(bookInstance => bookInstance.Id);
		modelBuilder.Entity<BookInstance>().Property(bookInstance => bookInstance.Id).ValueGeneratedOnAdd();
		modelBuilder.Entity<BookInstance>().Property(bookInstance => bookInstance.ISBN).HasMaxLength(17);
		modelBuilder.Entity<BookInstance>().Property(bookInstance => bookInstance.MemberId).IsRequired(false);

		modelBuilder.Entity<Member>().HasKey(member => member.Id);
		modelBuilder.Entity<Member>().Property(member => member.Id).ValueGeneratedOnAdd();
		modelBuilder.Entity<Member>().Property(member => member.Name)
			.IsRequired()
			.HasMaxLength(100);

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

		// Model Member to BookInstance one-to-many relationship.
		modelBuilder.Entity<Member>()
			.HasMany(member => member.BookInstances)
			.WithOne(bookInstance => bookInstance.Member)
			.HasForeignKey(bookInstance => bookInstance.MemberId)
			.IsRequired(false);
		
		modelBuilder.Entity<User>().HasKey(user => user.Id);
		modelBuilder.Entity<User>().Property(user => user.Id).ValueGeneratedOnAdd();
		modelBuilder.Entity<User>().Property(user => user.Name)
			.IsRequired()
			.HasMaxLength(100);
		modelBuilder.Entity<User>().Property(user => user.Username)
			.IsRequired()
			.HasMaxLength(100);
		
		modelBuilder.Entity<User>().HasIndex(user => user.Username).IsUnique();
	}
}