using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.TitleRelatedModels;

[Table("languages")]
[PrimaryKey(nameof(TitleId),nameof(OrderingAkas))]
public class Language
{
    [Column("language")]
    public string Langauge  { get; set; } = string.Empty;

    [Column("akas_id")]
    [Required]
    [ForeignKey(nameof(TitleKnownAs))]
    public string TitleId { get; set; }

    [Column("ordering")]
    [Required]
    [ForeignKey(nameof(TitleKnownAs))]
    public int OrderingAkas { get; set; }


    public TitleKnownAs? TitleKnownAs { get; set; }
}
