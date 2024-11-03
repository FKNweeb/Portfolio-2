using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Services;
using WebApi.DTO.TitleDtos;
using WebApi.Models.FunctionBasedModels;
using WebApi.Models.TitleRelatedModels;

using SearchResult = WebApi.Models.FunctionBasedModels.SearchResult;


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

   

    public static SearchResult ToSearchResultFromBestMatch(this BestMatch bestMatchObject)
    {
        return new SearchResult
        {
            tconst = bestMatchObject.tconst,
            primary_title = bestMatchObject.title,
        };
    }

    public static TitleAndGenre ToTitleAndGenre(this Title titleObject)
    {
        return new TitleAndGenre
        {
            title = titleObject.OriginalTitle
        };
    }

    public static TitleAndRating ToTitleAndRate(this Title titleObject)
    {
        return new TitleAndRating
        {
            Title = titleObject.PrimaryTitle,
            AverageRating = titleObject.AverageRating,
            NumberOfVotes = titleObject.NumberOfVotes,
        };
    }

    
}
