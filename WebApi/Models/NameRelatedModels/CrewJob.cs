using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Models.NameRelatedModels;

[Table("crew_job")]
[PrimaryKey(nameof(TitleId), nameof(Ordering))]
public class CrewJob
{
    [Required]
    [Column("tconst")]
    public string TitleId { get; set; }

    [Required]
    [Column("ordering")]
    public int Ordering { get; set; }    

    [Column("job")]
    
    public string JobTitle { get; set; }

    
    [ForeignKey(nameof(TitleId) + "," + nameof(Ordering))]
    public Crew? Crew { get; set; }
    
    [ForeignKey(nameof(JobTitle))]
    public Job? Job { get; set; }
}
