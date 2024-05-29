using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class AddUserViewModel : UserViewModel
{
    [Display(Name = "Password")]
    [Required(ErrorMessageResourceName = "PasswordIsRequired", 
        ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
    public string Password { get; init; }

    [Display(Name = "Repeat password")]
    [Required(ErrorMessageResourceName = "PasswordIsRequired", 
        ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
    public string RepeatPassword { get; init; }
}