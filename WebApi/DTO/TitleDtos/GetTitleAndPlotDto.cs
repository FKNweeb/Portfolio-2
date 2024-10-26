using WebApi.Models;

namespace WebApi.DTO.TitleDtos;

public class GetTitleAndPlotDto
{
    public string? PrimaryTitle { get; set; }

    public TitlePlot? TitlePlot { get; set; }
}
