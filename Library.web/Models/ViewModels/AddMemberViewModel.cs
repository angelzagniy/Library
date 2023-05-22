using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class AddMemberViewModel
{
    [Display(Name = "Full Name")]
    [Required(ErrorMessage = "Name is required.")]
    public string Name { get; set; }
}