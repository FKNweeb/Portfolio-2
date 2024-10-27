using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.TitleRelatedModels;

[Table("title_dates")]
public class TitleDate
{
    [Key]
    [ForeignKey(nameof(Title))]
    [Required]
    [Column("tconst")]  
    public string TitleId { get; set; }

    [Column("start_year")]
    public string StartYear { get; set; } = string.Empty;

    [Column("end_year")]
    public string EndYear { get; set; } = string.Empty;

    public Title? Title { get; set; }
}
