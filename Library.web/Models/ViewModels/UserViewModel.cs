using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Library.Web.BusinessLogic.Security;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Models.ViewModels;

public class UserViewModel
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
    
    /// <summary>
    /// Available roles.
    /// </summary>
    public IReadOnlyList<SelectListItem> Roles { get; init; } =
    [
        new SelectListItem(text: KnownRoles.User, value: KnownRoles.User, selected: true),
        new SelectListItem(text: KnownRoles.Admin, value: KnownRoles.Admin)
    ];
}