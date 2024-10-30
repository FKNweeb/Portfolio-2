using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.TitleRelatedModels;


namespace WebApi.Models.UserRelatedModels;

[Table("rate_title")]
[PrimaryKey(nameof(TitleId), nameof(UserId))]
public class RateTitle
{
    [Required]
    [Column("tconst")]
    public string TitleId { get; set; }

    [Required]
    [Column("user_id")]
    public string UserId { get; set; }

    [Column("votes")]
    public int Votes { get; set; }

    [ForeignKey(nameof(TitleId))]
    public Title Title { get; set; }
    
    public LocalTitleRating LocalTitleRating { get; set; }
}
