using System.Collections.Generic;

namespace Library.Web.Models.ViewModels;

public class AuthorPageViewModel: PageViewModel
{
    public AuthorPageViewModel(
        string title,
        IReadOnlyList<Author> authors)
        : base(title)
    {
        Authors = authors;
    }

    public IReadOnlyList<Author> Authors { get; }
    
    public string NameFilter { get; set; }
}