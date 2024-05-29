using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class LoginViewModel
{
	[Display(Name = "Username:")]
	[Required(ErrorMessageResourceName = "UserNameIsRequired", 
		ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
	public string Username { get; set; }
	
	[Display(Name = "Password:")]
	[Required(ErrorMessageResourceName = "PasswordIsRequired", 
		ErrorMessageResourceType = typeof(Library.Web.Resources.Shared))]
	public string Password { get; set; }
	
	public string ReturnUrl { get; set; }
}