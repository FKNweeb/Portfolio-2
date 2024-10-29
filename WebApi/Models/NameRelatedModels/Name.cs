using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.TitleRelatedModels;
namespace WebApi.Models.NameRelatedModels;

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

    public IList<KnownForTitle> KnownForTitles { get; set; }
    public IList<ProfessionName> ProfessionNames { get; set; }
    public IList<Crew> Crews { get; set; }
}
