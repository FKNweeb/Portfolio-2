using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.NameRelatedModels;

[Table("category")]
public class Category
{
    [Key]
    [Column("category")]
    public string CategoryName { get; set; }
    
    
    public Crew? Crew { get; set; }
 
}
