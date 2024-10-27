using WebApi.Models.TitleRelatedModels;

namespace WebApi.Interfaces;

public interface ITitlteRepository
{
    int NumberOfTitles();
    Task<List<Title>> GetAllAsync(int page, int pageSize);

    Task<List<Title>> GetAllTitleAndPlotAsync();
}
