using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.TitleRelatedModels;

[Table("title_poster")]
public class TitlePoster
{
    [Key]
    [Column("tconst")]
    [Required]
    [ForeignKey(nameof(Title))]
    public string TitleId { get; set; }

    [Column("poster")]
    public string PosterUrl { get; set; }

    public Title? Title { get; set; }
}
