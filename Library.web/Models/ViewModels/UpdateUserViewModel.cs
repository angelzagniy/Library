using System;

namespace Library.Web.Models.ViewModels;

public class UpdateUserViewModel : UserViewModel
{
    public Guid Id { get; init; }
}