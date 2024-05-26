using System.Collections.Generic;

namespace Library.Web.Models.ViewModels;

public class MembersPageViewModel : PageViewModel
{
    public MembersPageViewModel(
        string title,
        IReadOnlyList<Member> members)
        : base(title)
    {
        Members = members;
    }

    public IReadOnlyList<Member> Members { get; }
    public string NameFilter { get; set; }
}