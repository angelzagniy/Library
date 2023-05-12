namespace Library.Web.Models.ViewModels;

/// <summary>
/// View model for unexpected error.
/// </summary>
public class ErrorViewModel
{
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}