using Library.Web.Models;

namespace Library.Web;

public static class DbInitializer
{
	public static void Initialize(LibraryContext context)
	{
		context.Database.EnsureCreated();
		
		// Create members
		if (!context.Members.Any())
		{
			context.Members.AddRange(
				new Member
				{
					Name = "Debra Kyle"
				},
				new Member
				{
					Name = "John McQueen"
				},
				new Member
				{
					Name = "Tyron Bruno"
				});
		}

		// Look for any book.
		if (!context.Books.Any())
		{
			// Create authors
			Author authorOne = new ()
			{
				Name = "Olivia T. Burrow"
			};
			Author authorTwo = new ()
			{
				Name = "Erica P. Nelson"
			};
			Author authorThree = new ()
			{
				Name = "Samantha G. Shaffer"
			};
			
			context.Authors.AddRange(authorOne, authorTwo, authorThree);
			
			// Create books with references to authors.
			Book bookOne = new()
			{
				ISBN = "0-8566-8267-5",
				Title = "The Dynasty of Dawn",
				Genre = Genre.SkyFi,
				AuthorId = authorOne.Id,
				Year = 1998
			};

			Book bookTwo = new()
			{
				ISBN = "0-7670-5932-8",
				Title = "Year of Menace",
				Genre = Genre.Crime,
				AuthorId = authorOne.Id,
				Year = 2001
			};

			Book bookThree = new()
			{
				ISBN = "0-3794-3987-5",
				Title = "The Gun in the Lake",
				Genre = Genre.Crime,
				AuthorId = authorTwo.Id,
				Year = 2013
			};

			context.Books.AddRange(bookOne, bookTwo, bookThree);

			// Create book instances (paper copies) with references to books
			context.BookInstances.AddRange(
				new BookInstance
				{
					ISBN = bookOne.ISBN
				},
				new BookInstance
				{
					ISBN = bookOne.ISBN
				},
				new BookInstance
				{
					ISBN = bookOne.ISBN
				},
				new BookInstance
				{
					ISBN = bookTwo.ISBN
				},
				new BookInstance
				{
					ISBN = bookTwo.ISBN
				},
				new BookInstance
				{
					ISBN = bookThree.ISBN
				},
				new BookInstance
				{
					ISBN = bookThree.ISBN
				});

		}

		context.SaveChanges();
	}
}