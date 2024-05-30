using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Web.BusinessLogic.Abstract;
using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Web.BusinessLogic.Managers;

internal class BooksVmBuilder : IBooksVMBuilder
{
	private readonly IBooksRepository _booksRepository;
	private readonly IMembersRepository _membersRepository;

	public BooksVmBuilder(
		IBooksRepository booksRepository,
		IMembersRepository membersRepository)
	{
		_booksRepository = booksRepository;
		_membersRepository = membersRepository;
	}

	/// <inheritdoc />
	public async Task<LendBookViewModel> BuildLendBookViewModelAsync(Guid instanceId)
	{
		BookInstance instance = await _booksRepository.GetBookInstanceAsync(instanceId);
		IReadOnlyList<Member> members = await _membersRepository.ListMembersAsync();

		return new LendBookViewModel
		{
			BookInstance = instance,
			Members = members
		};
	}

	/// <inheritdoc />
	public async Task<BooksPageViewModel> BuildBooksPageViewModelAsync(
		string title,
		string author,
		Genre genre)
	{
		IReadOnlyList<Book> books = await _booksRepository.ListBooksAsync(
			title,
			author,
			genre);

		BooksPageViewModel viewModel = new("Books", books)
		{
			TitleFilter = title,
			GenreFilter = genre,
			AuthorFilter = author,
			Genres = Enum.GetValues<Genre>()
				.Select(g => new SelectListItem(
					text: LocalizationHelper.GetLocalizedGenre(g),
					value: g.ToString()))
				.ToArray()
		};

		return viewModel;
	}
}