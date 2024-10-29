using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.NameRelatedModels;

[Table("professions")]
public class Profession
{
    [Key]
    [Column("profession")]
    public string ProfessionTitle { get; set; }
    public IList<ProfessionName> ProfessionNames { get; set; }
}
