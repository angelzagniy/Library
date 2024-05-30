using System;
using System.Threading.Tasks;
using Library.Web.Models;
using Library.Web.Models.ViewModels;

namespace Library.Web.BusinessLogic.Abstract;

/// <summary>
/// Books management API.
/// </summary>
public interface IBooksVMBuilder
{
	public Task<LendBookViewModel> BuildLendBookViewModelAsync(Guid instanceId);
	
	Task<BooksPageViewModel> BuildBooksPageViewModelAsync(
		string title,
		string author,
		Genre genre);
}