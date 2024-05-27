using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class AddMemberViewModel
{
    [Display(Name = "Full Name")]
    [Required(ErrorMessage = "Member name is required.")]
    public string Name { get; init; }

    [Display(Name = "Phone Number")]
    [Required(ErrorMessage = "Phone number is required.")]
    [RegularExpression(
        pattern:@"^\+380\d{9}$",
        ErrorMessage = "Phone number should be in format +380#########")]
    public string PhoneNumber { get; init; }
}