using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.NameRelatedModels;

namespace WebApi.Models.UserRelatedModels;

[Table("bookmark_name")]
[PrimaryKey(nameof(UserId), nameof(NameId))]
public class BookMarkName
{
    [Column("user_id")]
    public string UserId { get; set; }

    [Column("nconst")]
    [ForeignKey(nameof(Name))]
    public string NameId { get; set; }

    public Name? Name { get; set; }
}
