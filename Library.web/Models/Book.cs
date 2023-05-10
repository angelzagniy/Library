namespace Library.web.Models;

public class Book
{
    public Book(string name, string genre, int year, int freeCount)
    {
        Name = name;
        Genre = genre;
        Year = year;
        FreeCount = freeCount;
    }

    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Genre { get; set; }
    
    public int Year { get; set; }
    
    public int FreeCount { get; set; }
}