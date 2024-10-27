using Npgsql.EntityFrameworkCore.PostgreSQL.Query.Expressions.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.TitleRelatedModels;

[Table("genre")]
public class Genre
{
    [Key]
    [Column("genre")]
    [Required]
    public string GenreName {  get; set; }

    public IList<TitleGenre> TitleGenres { get; set; }
}
