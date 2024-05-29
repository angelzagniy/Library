using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class AddMemberViewModel
{
    [Display(Name = "Full Name")]
    [Required(ErrorMessageResourceName = "MemberNameIsRequired", 
        ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
    public string Name { get; init; }

    [Display(Name = "Phone Number")]
    [Required(ErrorMessageResourceName = "PhoneNumberIsRequired", 
        ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
    [RegularExpression(
        pattern:@"^\+380\d{9}$",
        ErrorMessageResourceName = "PhoneNumberShouldBeInFormat", 
            ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
    public string PhoneNumber { get; init; }
}