using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Library.Web.Resources;
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

    public string AuthorName { get; init; }

    [Display(Name = "Books Number")]
    [Required(
        ErrorMessageResourceName = "BookNumberIsRequired",
        ErrorMessageResourceType = typeof(Shared))]
    [Range(1, 30,
        ErrorMessageResourceName = "BookNumberRange",
        ErrorMessageResourceType = typeof(Shared))]
    public int InstancesCount { get; init; }

    public IReadOnlyList<SelectListItem> Authors { get; init; }
}