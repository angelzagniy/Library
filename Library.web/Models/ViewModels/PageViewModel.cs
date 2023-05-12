namespace Library.Web.Models.ViewModels;

/// <summary>
/// Base class for page view model.
/// </summary>
public abstract class PageViewModel
{
	protected PageViewModel(string title)
	{
		Title = title;
	}

	public string Title { get; }
}