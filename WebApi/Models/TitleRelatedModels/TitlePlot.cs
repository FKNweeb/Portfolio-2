using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.TitleRelatedModels;


[Table("title_plot")]
public class TitlePlot
{
    [Key]
    [Required]
    [Column("tconst")]
    [ForeignKey(nameof(TitlePlotId))]
    public string TitlePlotId { get; set; }

    [Column("plot")]
    public string Plot { get; set; } = string.Empty;


    public Title? Title { get; set; }

}


