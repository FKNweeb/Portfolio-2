using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.TitleRelatedModels;

[Table("title_type")]
public class TitleType
{
    [Key]
    [Required]
    [Column("type_of_title")]
    public string Type { get; set; }

    public TitleIsType? TitleIsType { get; set; }

}
