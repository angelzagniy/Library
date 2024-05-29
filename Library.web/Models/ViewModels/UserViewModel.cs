using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Library.Web.BusinessLogic.Security;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Models.ViewModels;

public class UserViewModel
{
    [Display(Name = "Name")]
    [Required(ErrorMessageResourceName = "UserNameIsRequired", 
        ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
    public string Name { get; init; }
    
    [Display(Name = "Login name")]
    [Required(ErrorMessageResourceName = "LoginNameIsRequired", 
        ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
    public string UserName { get; init; }
    
    [Display(Name = "Role")]
    [Required(ErrorMessageResourceName = "RoleIsRequired", 
        ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
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