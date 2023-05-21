using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class AddAuthorViewModel
{
    [Display(Name = "Name")]
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}