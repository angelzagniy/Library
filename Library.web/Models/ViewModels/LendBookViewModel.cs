using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class LendBookViewModel
{
	public BookInstance BookInstance { get; set; }

	public IReadOnlyList<Member> Members { get; set; }

	[Required(ErrorMessage = "Select a library member")]
	public Guid? SelectedMemberId { get; set; }
}