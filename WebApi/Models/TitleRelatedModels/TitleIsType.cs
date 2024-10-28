using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.TitleRelatedModels;

[Table("title_is_types")]
[PrimaryKey(nameof(TitleId), nameof(TypeOfTitle))]
public class TitleIsType
{
    [Column("tconst")]
    [ForeignKey(nameof(Title))]
    public string TitleId { get; set; }

    [Column("type_of_title")]
    [ForeignKey(nameof(TitleType))]
    public string TypeOfTitle { get; set; }

    public Title? Title { get; set; }
    public TitleType? TitleType { get; set; }
}
