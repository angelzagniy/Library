using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class AddMemberViewModel
{
    [Display(Name = "Full Name")]
    [Required(ErrorMessage = "Member name is required.")]
    public string Name { get; set; }

    [Display(Name = "Phone Number")]
    [Required(ErrorMessage = "Phone number is required.")]
    public string PhoneNumber { get; set; }
}