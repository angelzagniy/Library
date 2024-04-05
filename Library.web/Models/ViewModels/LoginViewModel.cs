using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.ViewModels;

public class LoginViewModel : PageViewModel
{
	public LoginViewModel(string title = "Login")
		: base(title)
	{
	}
	
	[Display(Name = "Username:")]
	[Required(ErrorMessage = "Username is required.")]
	public string Username { get; init; }
	
	[Display(Name = "Password:")]
	[Required(ErrorMessage = "Password is required.")]
	public string Password { get; init; }
	
	public string ReturnUrl { get; init; }

}