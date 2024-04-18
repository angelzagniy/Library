using System;

namespace Library.Web.Models.ViewModels;

public class UpdateAuthorViewModel: AddAuthorViewModel
{
    public Guid Id { get; init; }
}