using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.UserRelatedModels;

[Table("has")]
[PrimaryKey(nameof(UserId), nameof(HistoryId))]

public class Has
{
    [Column("user_id")]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }

    [ForeignKey(nameof(SearchHistory))]
    [Column("history_id")]
    public int HistoryId { get; set; }

    public User? User { get; set; }
    public SearchHistory? SearchHistory { get; set; }

}
