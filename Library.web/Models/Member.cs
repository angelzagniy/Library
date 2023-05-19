namespace Library.Web.Models;

public class Member
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public ICollection<BookInstance> BookInstances { get; } = new List<BookInstance>(); 
}