using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WebApi.Models;

[Table("name")]
public class Name
{
    [Key]
    [Column("nconst")]
    [Required]
    public string NameId { get; set; }

    [Column("primary_name")]
    public string PrimaryName { get; set; } = string.Empty;

    [Column("birth_year")]
    public string BirthYear { get; set; }

    [Column("death_year")]
    public string DeathYear { get; set; }
}
