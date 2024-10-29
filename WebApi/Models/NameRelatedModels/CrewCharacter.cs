using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Models.NameRelatedModels;

[Table("crew_character")]
[PrimaryKey(nameof(TitleId), nameof(NameId), nameof(CharacterDescription))]
public class CrewCharacter
{
    [Column("tconst")]
    public string TitleId { get; set; }

    [Column("nconst")]
    public string NameId { get; set; }

    [Column("character_description")]
    public string CharacterDescription { get; set; }

    public Title Title { get; set; }
    public Name Name { get; set; }
}
