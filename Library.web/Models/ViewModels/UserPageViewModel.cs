using System.Collections.Generic;

namespace Library.Web.Models.ViewModels;

public class UserPageViewModel: PageViewModel
{
    public UserPageViewModel(
        string title,
        IReadOnlyList<User> users)
        : base(title)
    {
        Users = users;
    }

    public IReadOnlyList<User> Users { get; }
    public string NameFilter { get; set; }
}