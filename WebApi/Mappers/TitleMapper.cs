using System.Runtime.CompilerServices;
using WebApi.DTO.TitleDtos;
using WebApi.Models.TitleRelatedModels;
namespace WebApi.Mappers;

public static class TitleMapper
{
    public static TitleAndPlotDto ToTitleAndPlotDto(this Title titleObject)
    {
        return new TitleAndPlotDto
        {
            PrimaryTitle = titleObject.PrimaryTitle,
            Plot = titleObject.TitlePlot?.Plot.ToString(),
           
        };
    }


}
