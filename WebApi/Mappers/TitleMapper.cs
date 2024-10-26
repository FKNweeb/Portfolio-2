using System.Runtime.CompilerServices;
using WebApi.DTO.TitleDtos;
using WebApi.Models;
namespace WebApi.Mappers;

public static class TitleMapper
{
    public static GetAllTitleDto ToGetAllTitleDto(this Title titleObject)
    {
        return new GetAllTitleDto
        {
            PrimaryTitle = titleObject.PrimaryTitle,
            Plot = titleObject.TitlePlot?.Plot.ToString(),
           
        };
    }

    public static GetTitleAndPlotDto ToTitleAndPlotDto(this Title titleObject)
    {
        return new GetTitleAndPlotDto
        {
            PrimaryTitle = titleObject.PrimaryTitle,
            TitlePlot = titleObject.TitlePlot,

        };
    }
}
