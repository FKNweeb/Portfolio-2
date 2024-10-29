using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.TitleRelatedModels;

[Table("episode")]
public class Episode
{
    [Key]
    [Column("tconst")]
    [Required]
    public string TitleId { get; set; }

    [ForeignKey(nameof(TitleId))]
    public Title? Title { get; set; }

    [Column("parent_title")]
    public string ParentTitle {  get; set; }

    [Column("season_number")]
    public int SeasonNumber { get; set; }

    [Column("episode_number")]
    public int EpisodeNumber { get; set; }

}
