using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.NameRelatedModels;

namespace WebApi.Models.TitleRelatedModels;

[Table("known_for_title")]
[PrimaryKey(nameof(TitleId), nameof(NameId))]
public class KnownForTitle
{
    [Required]
    [Column("tconst")]
    [ForeignKey(nameof(Title))]
    public string TitleId { get; set; }

    [Required]
    [Column("nconst")]
    [ForeignKey(nameof(Name))]
    public string NameId { get; set; }

    public Title Title { get; set; }
    public Name Name { get; set; }
}
