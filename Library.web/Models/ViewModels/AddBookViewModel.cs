using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Models.ViewModels;

/// <summary>
/// View model for adding new book.
/// </summary>
public class AddBookViewModel : BookViewModel
{
    [Display(Name = "Author")]
    [Required(ErrorMessage = "Author is required.")]
    public Guid? AuthorId { get; init; }

    [Display(Name = "Books Number")]
    [Range(1, 30, ErrorMessage = "The number of books should be between 1 and 30.")]
    public int InstancesCount { get; init; }

    public IReadOnlyList<SelectListItem> Authors { get; init; }
}