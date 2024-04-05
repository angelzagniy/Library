using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Web.BusinessLogic.Abstract;
using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Library.Web.Models.ViewModels;

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
}