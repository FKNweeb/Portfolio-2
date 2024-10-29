using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.TitleRelatedModels;

[Table("languages")]
[PrimaryKey(nameof(TitleId),nameof(OrderingAkas))]
public class Language
{
    [Column("language")]
    public string LanguageUsed { get; set; } = string.Empty;

    [Column("akas_id")]
    [Required]
    //[ForeignKey(nameof(TitleKnownAs))]
    public string TitleId { get; set; }

    [Column("ordering")]
    [Required]
    //[ForeignKey(nameof(TitleKnownAs))]
    public int OrderingAkas { get; set; }

    [ForeignKey(nameof(TitleId) + "," + nameof(OrderingAkas))]
    public TitleKnownAs? TitleKnownAs { get; set; }
}
