namespace Library.Web.Models;

public class Member
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }

    public List<BookInstance> Books { get; set; }
}