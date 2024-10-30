using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Models.UserRelatedModels;

[Table("local_title_rating")]
public class LocalTitleRating
{
    [Key]
    [Required]
    [Column("tconst")]
    public string TitleId { get; set; }

    [Column("average_rating")]
    public double AverageRating { get; set; }

    [Column("total_votes")]
    public int TotalVotes { get; set; }
    
    public IList<RateTitle> RateTitles { get; set; }
}
