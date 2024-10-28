using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Models;

[Table("wi")]
[PrimaryKey(nameof(TitleId), nameof(Word), nameof(Field))]
public class WordIndex
{
    [Required]
    [Column("tconst")]
    [ForeignKey(nameof(Title))]
    public string TitleId { get; set; }

    [Required]
    [Column("word")]
    public string Word {  get; set; }
    
    [Required]
    [Column("field")]
    public string Field { get; set; }

    public Title? Title { get; set; }
}
