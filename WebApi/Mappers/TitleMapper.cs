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
            EndDate = titleObject.TitleDate?.EndYear.ToString(),
            //Genres = titleObject.TitleGenres?.Select(g => g.Genre.GenreName).ToList(),
            Genres = titleObject.TitleGenres.Any() ? titleObject.TitleGenres.Where(g => g.GenreName != null).Select(g => g.Genre.GenreName).ToList() : null,
            Languages = titleObject.TitleKnownAs.Any() ? titleObject.TitleKnownAs.Select(l => l.Language?.LanguageUsed).ToList() : null,
            Poster = titleObject.TitlePoster?.PosterUrl.ToString(),
            //Type = titleObject.TitleIsType?.TypeOfTitle?.ToString(),
            Type = !string.IsNullOrEmpty(titleObject.TitleIsType?.TypeOfTitle) ? titleObject.TitleIsType.TypeOfTitle.ToString() : null,


        }; ;
    }

    public static TitleAndGenreDTO ToTitleAndGenreDto(this Title titleObject)
    {
        return new TitleAndGenreDTO
        {
            PrimaryTitle = titleObject.PrimaryTitle,
            Plot = titleObject.TitlePlot?.Plot.ToString(),
            Genre = titleObject.TitleGenres.Select(tg => tg.Genre.GenreName).ToList()
        };
    }

    
}
