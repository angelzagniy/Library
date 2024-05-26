using System;

namespace Library.Web.Models.ViewModels;

public class UpdateMemberViewModel: AddMemberViewModel
{
    public Guid Id { get; init; }
}