using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.UserRelatedModels;

[Table("local_name_rating")]
public class LocalNameRating
{
    [Key]
    [Column("nconst")]
    public string NameId { get; set; }

    [Column("average_rating")]
    public double AverageRating { get; set; }

    [Column("total_vote")]
    public int TotalVotes { get; set; }

    public IList<RateName?> RateNames { get; set; }
}
