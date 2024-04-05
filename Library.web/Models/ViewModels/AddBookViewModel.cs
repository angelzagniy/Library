using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Models.ViewModels;

public class AddBookViewModel
{
	[Display (Name = "ISBN")]
	[Required(ErrorMessage = "ISBN is required.")]
	[RegularExpression(
		pattern: "^(?:ISBN(?:-1[03])?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]$",
		ErrorMessage = "The format should ISBN 10 or 13.")]
	public string ISBN { get; set; }

	[Display(Name = "Book Title")]
	[Required(ErrorMessage = "Book title is required.")]
	[MaxLength(100, ErrorMessage = "Book title should not exceed 100 characters.")]
	public string Title { get; set; }

	[Display(Name = "Genre")]
	[Required(ErrorMessage = "Genre is required.")]
	public Genre? Genre { get; set; }
	
	[Display(Name = "Publish Year")]
	[Range(1900, 2023, ErrorMessage = "Publish year should be between 1800 and 2023.")]
	[DefaultValue(2000)]
	public int Year { get; set; }

	[Display(Name = "Author")]
	[Required(ErrorMessage = "Author is required.")]
	public Guid? AuthorId { get; set; }
	
	[Display(Name = "Books Number")]
	[Range(1, 30, ErrorMessage = "The number of books should be between 1 and 30.")]
	public int InstancesCount { get; set; }

	public IReadOnlyList<SelectListItem> Genres { get; set; }

	public IReadOnlyList<SelectListItem> Authors { get; set; }
}