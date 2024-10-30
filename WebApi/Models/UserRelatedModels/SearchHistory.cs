using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.UserRelatedModels;

[Table("search_history")]
public class SearchHistory
{
    [Key]
    [Required]
    [Column("history_id")]
    
    public int HistoryId { get; set; }

    [Column("description")]
    public string Description { get; set; }
    public IList<Has?> Has { get; set; }

}
