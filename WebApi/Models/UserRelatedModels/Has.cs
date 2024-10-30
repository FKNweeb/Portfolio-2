using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.UserRelatedModels;

[Table("has")]
public class Has
{

    public int UserId { get; set; }

    
    public int HistoryId { get; set; }
}
