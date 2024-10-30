using WebApi.Models;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Interfaces;

public interface ITitlteRepository
{
    int NumberOfTitles();
    Task<List<Title>> GetAllAsync(int page, int pageSize);

    Task<List<Title>> GetAllTitlesByDate(string startyear, int page, int pageSize);

    Task<List<Title>> GetAllTitlesByType(int page, int pageSize);

    Task<List<Title>> GetAllTitlesWithPoster(int page, int pageSize);

    Task<List<Title>> GetTitleAndWordIndex(string title, int page, int pageSize);

    Task<List<TitleKnownAs>> GetTilteByLanguage(int page, int pageSize);

    Task<List<Episode>> GetEpisodesByParentTitel(string id, int page, int pageSize);
    Task<List<Title>> GetRateTitle(string id, int page, int pageSize);
    
}
