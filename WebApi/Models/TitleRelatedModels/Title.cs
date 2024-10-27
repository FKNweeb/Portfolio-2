using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApi.Models.TitleRelatedModels;

[Table("title")]
public class Title
{
    [Key]
    [Column("tconst")]
    [Required]
    public string TitleId { get; set; }

    [Column("primary_title")]
    public string PrimaryTitle { get; set; } = string.Empty;

    [Column("original_title")]
    public string OriginalTitle { get; set; } = string.Empty;

    [Column("runtime_minutes")]
    public int? RuntimeMinutes { get; set; }

    [Column("average_rating")]
    public double? AverageRating { get; set; }

    [Column("num_votes")]
    public int? NumberOfVotes { get; set; }


    public List<TitleKnowAs?> TitleKnownAs { get; set; }

    public TitlePlot? TitlePlot { get; set; }

    public IList<TitleGenre> TitleGenres { get; set; }

    public TitleDate? TitleDate { get; set; }

}
