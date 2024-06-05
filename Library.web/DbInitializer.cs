using Library.Web.BusinessLogic.Security;
using System.Linq;
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
				// Welcome123
				Password = "56C3A3C8E729FF4F81D0EAD9E687BC7DBA5E4C9E72400F024AC40D62445DDBCFEC429D234CE0FE42E88166841742FACC977A9CE032C82046E1FC3B6DDB860BD8",
				Salt = "B9DDCB58B1649527D74B9836B236CA5ECDD2781D38DABB6334FCAA1DCDDA4CA6CC694BB3F8A5A868DCBC833306D32C49FF69945C1500850ADF71685B23383528",
				Role = KnownRoles.Admin,
				IsBuiltIn = true
			};

			context.Users.Add(admin);
			context.SaveChanges();
		}

		// Look for any book.
		if (!context.Books.Any())
		{
			Member memberOne = new()
			{
				Name = "Кулеба Іван Анатолійович",
				PhoneNumber = "+380967778901"
			};
			Member memberTwo = new()
			{
				Name = "Петриченко Вікторія Сергіївна",
				PhoneNumber = "+380977798922"
			};
			Member memberThree = new()
			{
				Name = "Голик Олена Ігорівна",
				PhoneNumber = "+380508893458"
			};

			context.Members.AddRange(memberOne, memberTwo, memberThree);
			
			// Create authors
			Author authorOne = new ()
			{
				Name = "Стівен Кінг"
			};
			Author authorTwo = new ()
			{
				Name = "Агата Крісті"
			};
			Author authorThree = new ()
			{
				Name = "Сесілія Ахерн"
			};
			
			context.Authors.AddRange(authorOne, authorTwo, authorThree);
			
			// Create books with references to authors.
			Book bookOne = new()
			{
				ISBN = "1-5387-4660-3",
				Title = "P.S. Я тебе кохаю",
				Genre = Genre.Novel,
				AuthorId = authorThree.Id,
				Year = 2003
			};

			Book bookTwo = new()
			{
				ISBN = "5-0412-4492-8",
				Title = "Зелена миля",
				Genre = Genre.Mystery,
				AuthorId = authorOne.Id,
				Year = 2001
			};

			Book bookThree = new()
			{
				ISBN = "5-4576-4175-9",
				Title = "Містер Мерседес",
				Genre = Genre.Crime,
				AuthorId = authorOne.Id,
				Year = 2014
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