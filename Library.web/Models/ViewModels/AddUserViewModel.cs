using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class AddUserViewModel
{
    [Display(Name = "Name")]
    [Required(ErrorMessage = "User name is required.")]
    public string Name { get; init; }
    
    [Display(Name = "Login name")]
    [Required(ErrorMessage = "Login name is required.")]
    public string UserName { get; init; }
    
    [Display(Name = "Role")]
    [Required(ErrorMessage = "Role is required.")]
    public string Role { get; init; }
    
    [Display(Name = "Password")]
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; init; }
}