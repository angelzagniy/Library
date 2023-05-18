using Library.Web.Models;

namespace Library.Web;

public static class DbInitializer
{
	public static void Initialize(LibraryContext context)
	{
		context.Database.EnsureCreated();

		// Look for any book.
		if (!context.Books.Any())
		{
			Book bookOne = new()
			{
				ISBN = "0-8566-8267-5",
				Title = "The Dynasty of Dawn",
				Genre = Genre.SkyFi,
				Author = "Olivia T. Burrow",
				Year = 1998
			};

			Book bookTwo = new()
			{
				ISBN = "0-7670-5932-8",
				Title = "Year of Menace",
				Genre = Genre.Crime,
				Author = "Erica P. Nelson",
				Year = 2001
			};

			Book bookThree = new()
			{
				ISBN = "0-3794-3987-5",
				Title = "The Gun in the Lake",
				Genre = Genre.Crime,
				Author = "Samantha G. Shaffer",
				Year = 2013
			};

			context.Books.AddRange(bookOne, bookTwo, bookThree);

			context.BookInstances.AddRange(
				new BookInstance
				{
					Book = bookOne
				},
				new BookInstance
				{
					Book = bookOne
				},
				new BookInstance
				{
					Book = bookOne
				},
				new BookInstance
				{
					Book = bookTwo
				},
				new BookInstance
				{
					Book = bookTwo
				},
				new BookInstance
				{
					Book = bookThree
				},
				new BookInstance
				{
					Book = bookThree
				});
		}

		if (!context.Members.Any())
		{
			context.Members.AddRange(
				new Member
				{
					Name = "Debra",
					Surname = "Kyle"
				},
				new Member
				{
					Name = "John",
					Surname = "McQueen"
				},
				new Member
				{
					Name = "Tyron",
					Surname = "Bruno"
				});
		}

		context.SaveChanges();
	}
}