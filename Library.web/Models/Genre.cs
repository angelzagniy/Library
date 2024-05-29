using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models;

/// <summary>
/// Book genres.
/// </summary>
public enum Genre
{
	Any = 0,
	Novel = 1,
	Mystery = 2,
	[Display(Name = "Science Fiction")]
	ScienceFiction = 4,
	[Display(Name = "Historical Fiction")]
	HistoricalFiction = 5,
	Crime = 7
}