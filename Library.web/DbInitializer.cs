
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
            context.Books.AddRange(new[]
            {
                new Book
                {
                    Name = "The Dynasty of Dawn",
                    Genre = Genre.SkyFi,
                    Author = "Olivia T. Burrow",
                    Year = 1998
                },
                new Book
                {
                    Name = "Year of Menace",
                    Genre = Genre.Crime,
                    Author = "Erica P. Nelson",
                    Year = 2001
                },
                new Book
                {
                    Name = "The Gun in the Lake",
                    Genre = Genre.Crime,
                    Author = "Samantha G. Shaffer",
                    Year = 2013
                }

            });
        }

        if (!context.Members.Any())
        {
        }

        context.SaveChanges();
    }
}