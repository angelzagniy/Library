using Library.Web.Models;

namespace Library.Web.BusinessLogic;

internal static class LocalizationHelper
{
    public static string GetLocalizedGenre(Genre genre) =>
        Resources.Shared.ResourceManager.GetString($"Genre_{genre}") ?? genre.ToString();
}