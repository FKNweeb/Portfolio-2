﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApi.Models.TitleRelatedModels;

[Table("akas")]
[PrimaryKey(nameof(TitleId), nameof(OrderingAkas))]
public class TitleKnownAs
{
    [Column("akas_id")]
    [Required]
    [ForeignKey(nameof(TitleId))]
    public string TitleId { get; set; }

    [Column("ordering")]
    [Required]
    public int OrderingAkas { get; set; }

    [Column("title")]
    [Required]
    public string KnownAsTitle { get; set; }

    [Column("type")]
    [Required]
    public string Type { get; set; } = string.Empty;

    public Title? Title { get; set; }

    public Language? Language { get; set; }

    public Region Region { get; set; }
}
