using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.NameRelatedModels;

[Table("profession_name")]
[PrimaryKey(nameof(ProfessionTitle), nameof(NameId))]
public class ProfessionName
{
    [Column("profession")]
    [ForeignKey(nameof(ProfessionTitle))]
    public string ProfessionTitle { get; set; }
    
    [Column("nconst")]
    [ForeignKey(nameof(Name))]
    public string NameId { get; set; }
    
    public Name Name { get; set; }
    
    public Profession Profession { get; set; }
}
