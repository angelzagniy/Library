using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

/// <summary>
/// View model for adding new book.
/// </summary>
public class AddBookViewModel : BookViewModel
{
    [Display(Name = "Books Number")]
    [Range(1, 30, ErrorMessage = "The number of books should be between 1 and 30.")]
    public int InstancesCount { get; init; }
}