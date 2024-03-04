using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class AddAuthorViewModel
{
    [Display(Name = "Name")]
    [Required(ErrorMessage = "Author name is required.")]
    public string Name { get; init; }
}