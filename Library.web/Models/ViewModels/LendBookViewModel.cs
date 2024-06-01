using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.Models.ViewModels;

public class LendBookViewModel
{
	public BookInstance BookInstance { get; set; }

	[Required(ErrorMessage = "Select a library member")]
	public Guid? SelectedMemberId { get; set; }
	
	public string MemberName { get; set; }

	public IReadOnlyList<SelectListItem> Members { get; set; }
}