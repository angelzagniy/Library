using System;
using System.Linq;
using System.Threading.Tasks;
using Library.Web.BusinessLogic.Managers;
using Library.Web.BusinessLogic.Repository.Abstract;
using Library.Web.Models;
using Library.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using NUnit.Framework;

namespace Library.Tests;

[TestFixture]
public class BooksVmBuilderTests
{
	private Mock<IBooksRepository> _bookRepository;
	private Mock<IMembersRepository> _membersRepository;

	private BooksVmBuilder Subject => new(
		_bookRepository.Object,
		_membersRepository.Object);

	[SetUp]
	public void SetUp()
	{
		_bookRepository = new Mock<IBooksRepository>();
		_membersRepository = new Mock<IMembersRepository>();
	}
	
	[Test]
	public async Task ShouldReturnEmptyModelIfNoDataExists()
	{
		Guid instanceId = Guid.NewGuid();

		_bookRepository
			.Setup(repository => repository.GetBookInstanceAsync(It.IsAny<Guid>()))
			.ReturnsAsync((BookInstance)null);

		_membersRepository
			.Setup(repository => repository.ListMembersAsync(It.IsAny<string>()))
			.ReturnsAsync(Array.Empty<Member>());

		LendBookViewModel viewModelAsync = await Subject.BuildLendBookViewModelAsync(instanceId);

		Assert.That(viewModelAsync, Is.Not.Null);
		Assert.That(viewModelAsync.BookInstance, Is.Null);
		Assert.That(viewModelAsync.Members, Is.Empty);

		_bookRepository.Verify(
			repository => repository.GetBookInstanceAsync(instanceId),
			Times.Once,
			"Book repository should be called once.");
		
		_membersRepository.Verify(
			repository => repository.ListMembersAsync(It.IsAny<string>()),
			Times.Once,
			"Members repository should be called once.");
	}

	[Test]
	public async Task ShouldBuildLendBookViewModel()
	{
		Guid instanceId = Guid.NewGuid();
		Guid memberOneId = Guid.NewGuid();
		Guid memberTwoId = Guid.NewGuid();
		string isbn = Guid.NewGuid().ToString();

		_bookRepository
			.Setup(repository => repository.GetBookInstanceAsync(instanceId))
			.ReturnsAsync(
				new BookInstance
				{
					Id = instanceId,
					ISBN = isbn,
					Book = new Book
					{
						ISBN = isbn
					}
				});

		_membersRepository
			.Setup(repository => repository.ListMembersAsync(null))
			.ReturnsAsync(new[]
			{
				new Member
				{
					Id = memberOneId
				},
				new Member
				{
					Id = memberTwoId
				}
			});

		LendBookViewModel viewModel = await Subject.BuildLendBookViewModelAsync(instanceId);

		Assert.That(viewModel, Is.Not.Null);
		Assert.That(viewModel.BookInstance, Is.Not.Null);
		Assert.That(viewModel.BookInstance.Id, Is.EqualTo(instanceId));
		Assert.That(viewModel.BookInstance.ISBN, Is.EqualTo(isbn));
		Assert.That(viewModel.BookInstance.Book, Is.Not.Null);
		Assert.That(viewModel.Members, Is.Not.Null);
		Assert.That(viewModel.Members, Has.Count.EqualTo(2));

		SelectListItem memberOne = viewModel.Members
			.SingleOrDefault(m => m.Value == memberOneId.ToString());
		Assert.That(memberOne, Is.Not.Null);
		
		_bookRepository.Verify(
			repository => repository.GetBookInstanceAsync(instanceId),
			Times.Once,
			"Book repository should be called once.");
	}
}