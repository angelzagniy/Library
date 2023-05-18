namespace Library.Web.Models;

public class Member
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public List<BookInstance> Books { get; set; }
}