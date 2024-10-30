using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.UserRelatedModels;

[Table("has")]
[PrimaryKey(nameof(UserId), nameof(SearchHistory))]
[NotMapped]
public class Has
{
    [Column("user_id")]
    public int UserId { get; set; }

    [ForeignKey(nameof(SearchHistory))]
    [Column("history_id")]
    public int HistoryId { get; set; }

    public SearchHistory? SearchHistory { get; set; }
}
