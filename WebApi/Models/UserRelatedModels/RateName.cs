using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.NameRelatedModels;

namespace WebApi.Models.UserRelatedModels;

[Table("rate_name")]
[PrimaryKey(nameof(UserId), nameof(NameId))]
public class RateName
{
    [Required]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required]
    [Column("nconst")]
    [ForeignKey(nameof(LocalNameRating))]
    public string NameId { get; set; }

    [Column("vote")]
    public int Vote { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    
    [ForeignKey(nameof(NameId))]
    public Name Name { get; set; }

    public LocalNameRating? LocalNameRating { get; set; } 
}
