using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.UserRelatedModels;

[Table("users")]

public class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }
    [Column("user_name")]
    public string UserName { get; set; }
    [Column("user_password")]
    public string UserPassword { get; set; }
    [Column("user_email")]
    public string UserEmail { get; set; }
   
}
