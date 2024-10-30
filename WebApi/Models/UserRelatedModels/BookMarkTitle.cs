using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Models.UserRelatedModels;

[Table("bookmark_title")]
[PrimaryKey(nameof(UserId), nameof(TitleId))]
public class BookMarkTitle
{
    [Column("user_id")]
    [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }

    public User? User { get; set; } 

    [Column("tconst")]
    [ForeignKey(nameof(Title))]
    public string TitleId { get; set; }

    public Title? Title { get; set; }

}
