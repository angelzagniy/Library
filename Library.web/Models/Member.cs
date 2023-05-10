namespace Library.web.Models;

public class Member
{
    public Member(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }
    
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Surname { get; set; }

    public List<Book> Books;
}