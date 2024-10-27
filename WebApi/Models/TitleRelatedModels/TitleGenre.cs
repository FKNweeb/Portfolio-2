using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.TitleRelatedModels;


[Table("title_genre")]
[PrimaryKey(nameof(TitleId), nameof(GenreName))]
public class TitleGenre
{
    [ForeignKey(nameof(Title))]
    [Column("tconst")]
    public string TitleId { get; set; }

    [ForeignKey(nameof(Genre))]
    [Column("genre")]
    public string GenreName { get; set; } = string.Empty;

    public Title? Title { get; set; }
    public Genre? Genre { get; set; }

}
