using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Models.ViewModels;

/// <summary>
/// Base view model for books
/// </summary>
public class BookViewModel
{
    [Display(Name = "ISBN")]
    [Required(ErrorMessage = "ISBN is required.")]
    [RegularExpression(
        pattern:
        "^(?:ISBN(?:-1[03])?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]$",
        ErrorMessage = "The format should ISBN 10 or 13.")]
    public string ISBN { get; init; }

    [Display(Name = "Book Title")]
    [Required(ErrorMessage = "Book title is required.")]
    [MaxLength(100, ErrorMessage = "Book title should not exceed 100 characters.")]
    public string Title { get; init; }

    [Display(Name = "Genre")]
    [Required(ErrorMessage = "Genre is required.")]
    public Genre? Genre { get; init; }

    [Display(Name = "Publish Year")]
    [Range(1900, 2023, ErrorMessage = "Publish year should be between 1800 and 2023.")]
    [DefaultValue(2000)]
    public int Year { get; init; }

    [Display(Name = "Author")]
    [Required(ErrorMessage = "Author is required.")]
    public Guid? AuthorId { get; init; }

    public IReadOnlyList<SelectListItem> Genres { get; }

    public IReadOnlyList<SelectListItem> Authors { get; init; }

    public BookViewModel()
    {
        Genres = Enum.GetValues<Genre>()
            .Where(g => g != Models.Genre.Any)
            .Select(genre => new SelectListItem(genre.ToString(), genre.ToString()))
            .ToArray();
    }
}