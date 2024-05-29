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
    [Required(
        ErrorMessageResourceName = "AuthorIsRequired",
    ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
    public Guid? AuthorId { get; init; }

    public string AuthorName { get; init; }

    [Display(Name = "Books Number")]
    [Required(
        ErrorMessageResourceName = "BookNumberIsRequired",
        ErrorMessageResourceType = typeof(Resources.Shared))]
    [Range(1, 30,
        ErrorMessageResourceName = "BookNumberRange",
        ErrorMessageResourceType = typeof(Resources.Shared))]
    public int InstancesCount { get; init; }

    public IReadOnlyList<SelectListItem> Authors { get; init; }
}