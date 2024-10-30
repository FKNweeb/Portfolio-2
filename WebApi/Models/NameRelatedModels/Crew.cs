using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Models.NameRelatedModels;

[Table("crew")]
[PrimaryKey(nameof(TitleId), nameof(Ordering))]
public class Crew
{

    [Column("tconst")]
    [Required]
    public string TitleId { get; set; }

    [Column("ordering")]
    [Required]
    public int Ordering { get; set; }
    [Column("nconst")]
    public string NameId { get; set; }

    [Column("category")]
    public string Category { get; set; }
    public Name Name { get; set; }
    public Title Title { get; set; }
    public CrewJob CrewJob { get; set; }
}

