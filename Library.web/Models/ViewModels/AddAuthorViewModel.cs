using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class AddAuthorViewModel
{
    [Display(Name = "Name")]
    [Required(ErrorMessageResourceName = "AuthorNameIsRequired", 
        ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
    public string Name { get; init; }
}