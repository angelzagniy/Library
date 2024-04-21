using System;

namespace Library.Web.Models.ViewModels;

public class UpdateBookViewModel: AddBookViewModel
{
    public string ISBN { get; init; }
}