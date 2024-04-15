using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class AddUserViewModel : UserViewModel
{
    [Display(Name = "Password")]
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; init; }

    [Display(Name = "Repeat password")]
    [Required(ErrorMessage = "Password is required.")]
    public string RepeatPassword { get; init; }
}