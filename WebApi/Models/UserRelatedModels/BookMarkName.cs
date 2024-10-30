﻿using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.NameRelatedModels;

namespace WebApi.Models.UserRelatedModels;

[Table("bookmark_name")]
public class BookMarkName
{
    [Column("")]
    public string UserId { get; set; }

    [Column("user_id")]
    [ForeignKey(nameof(Name))]
    public string NameId { get; set; }

    public Name? Name { get; set; }
}
