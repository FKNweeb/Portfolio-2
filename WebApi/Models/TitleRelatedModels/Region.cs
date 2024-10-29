using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.TitleRelatedModels;

[Table("region")]
[PrimaryKey(nameof(RegionId), nameof(RegionOrdering))]
public class Region
{
    [Column("akas_id")]
    public string RegionId { get; set; }

    [Column("ordering")]
    public int RegionOrdering {  get; set; }

    [Column("country")]
    public string Country { get; set; }

    [ForeignKey(nameof(RegionId) + "," + nameof(RegionOrdering))]
    public TitleKnownAs TitleKnownAs { get; set; }

}
