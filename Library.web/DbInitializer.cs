using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Library.Web;

public class DbInitializer
{
    public static void Initialize(LibraryContext context)
    {
        context.Database.EnsureCreated();
        
        var databaseCreator = context.GetService<IRelationalDatabaseCreator>();

        // Look for any students.
        if (context.Books.Any())
        {
            return; // DB has been seeded
        }

        if (context.Members.Any())
        {
            return;
        }

        context.SaveChanges();
    }
}