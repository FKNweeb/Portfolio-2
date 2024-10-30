using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Models.NameRelatedModels;

[Table("jobs")]
public class Job
{
    [Key]
    [Required]
    [Column("job")]
    public string JobTitle { get; set; }

    public IList<CrewJob> CrewJobs { get; set; }
}
