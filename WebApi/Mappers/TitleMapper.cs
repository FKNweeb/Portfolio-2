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
            StartDate = titleObject.TitleDate?.StartYear.ToString(),
            EndDate = titleObject.TitleDate?.EndYear.ToString()

        };
    }

    public static TitleAndGenre ToTitleAndGenreDto(this Title titleObject)
    {
        return new TitleAndGenre
        {
            PrimaryTitle = titleObject.PrimaryTitle,
            Plot = titleObject.TitlePlot?.Plot.ToString(),
            Genre = titleObject.TitleGenres.Select(tg => tg.Genre.GenreName).ToList()
        };
    }

    public static TitleAndDateDto ToTitleAndDateDto(this Title titleObject)
    {
        return new TitleAndDateDto
        {
            PrimaryTitle = titleObject.PrimaryTitle,
            StartDate = titleObject.TitleDate?.StartYear.ToString(),
            EndDate  =titleObject.TitleDate?.EndYear.ToString()
        };
    }
}
