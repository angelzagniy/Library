using Library.Web.BusinessLogic.Security;
using Library.Web.Models;

namespace Library.Web;

public static class DbInitializer
{
	public static void Initialize(LibraryContext context)
	{
		context.Database.EnsureCreated();

		if (!context.Users.Any())
		{
			User admin = new()
			{
				Name = "Built-in Admin",
				Username = "Admin",
				Password = "56C3A3C8E729FF4F81D0EAD9E687BC7DBA5E4C9E72400F024AC40D62445DDBCFEC429D234CE0FE42E88166841742FACC977A9CE032C82046E1FC3B6DDB860BD8",
				Salt = "B9DDCB58B1649527D74B9836B236CA5ECDD2781D38DABB6334FCAA1DCDDA4CA6CC694BB3F8A5A868DCBC833306D32C49FF69945C1500850ADF71685B23383528",
				Role = KnownRoles.Admin
			};

			context.Users.Add(admin);
			context.SaveChanges();
		}

		// Look for any book.
		if (!context.Books.Any())
		{
			Member memberOne = new()
			{
				Name = "Sylvia R. Torres"
			};
			Member memberTwo = new()
			{
				Name = "Patricia M. Rook"
			};
			Member memberThree = new()
			{
				Name = "Ellen D. Hollars"
			};

			context.Members.AddRange(memberOne, memberTwo, memberThree);
			
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
				Genre = Genre.SinceFiction,
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
					ISBN = bookOne.ISBN,
					MemberId = memberOne.Id
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

			context.SaveChanges();
		}
	}
}