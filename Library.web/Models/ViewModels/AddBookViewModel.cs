using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Models.ViewModels;

public class AddBookViewModel
{
	[Display (Name = "ISBN")]
	[Required(ErrorMessage = "Book identifier is required.")]
	public string ISBN { get; set; }

	[Display(Name = "Book Title")]
	[Required(ErrorMessage = "Book title is required.")]
	public string Title { get; set; }

	[Display(Name = "Genre")]
	public Genre Genre { get; set; }
	
	[Display(Name = "Published Year")]
	[Required(ErrorMessage = "Year is required")]
	[Range(1900, 2023)]
	[DefaultValue(2000)]
	public int Year { get; set; }

	[Display(Name = "Author")]
	public Guid AuthorId { get; set; }
	
	[Display(Name = "Books Number")]
	public int InstancesCount { get; set; }
	
	public IReadOnlyList<SelectListItem> Authors { get; set; }
}