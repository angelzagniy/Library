using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

using Library.Web.Resources;

namespace Library.Web.Models.ViewModels;

/// <summary>
/// Base view model for books
/// </summary>
public class BookViewModel
{
    [Display(Name = "ISBN")] 
    [Required(ErrorMessageResourceName = "ISBNisRequired",
        ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
    [RegularExpression(
        pattern:
        "^(?:ISBN(?:-1[03])?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]$",
        ErrorMessageResourceName = "TheFormatShouldISBN10or13", ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
    public string ISBN { get; init; }

    [Display(Name = "Book Title")]
    [Required(ErrorMessageResourceName = "BookTitleIsRequired",
        ErrorMessageResourceType = typeof(Resources.Shared))]
    [MaxLength(100, ErrorMessageResourceName = "BookTitleShouldNotExceed100Characters", ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
    public string Title { get; init; }

    [Display(Name = "Genre")]
    [Required(ErrorMessageResourceName = "GenreIsRequired",
        ErrorMessageResourceType = typeof(Resources.Shared))]
    public Genre? Genre { get; init; }

    [Display(Name = "Publish Year")]
    [Range(1800, 2024, ErrorMessageResourceName = "PublishYearShouldBeBetween1800and2024",
        ErrorMessageResourceType = typeof(Resources.Shared))]
    [DefaultValue(2000)]
    public int Year { get; init; }

    public IReadOnlyList<SelectListItem> Genres { get; }

    public BookViewModel()
    {
        Genres = Enum.GetValues<Genre>()
            .Where(g => g != Models.Genre.Any)
            .Select(genre => new SelectListItem(text: genre.ToString(), value: genre.ToString()))
            .ToArray();
    }
}