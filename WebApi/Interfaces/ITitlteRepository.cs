
using System.Threading.Tasks;
using WebApi.DTO.TitleDtos;
using WebApi.Models;
using WebApi.Models.FunctionBasedModels;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Interfaces;

public interface ITitlteRepository
{
    int NumberOfTitles();
    Task<List<Title>> GetAllAsync(int page, int pageSize);

    Task<Title?> GetTitleById(string id);

    Task<List<Title>> GetTitleAndRating(QueryRating queryObject, int page, int pageSize);

    Task<List<SearchResult>> SearchWithKeyword(string keyword,int page,int pageSize);
    int NumberOfTitlesPerKeyword(string keyword);

    Task<List<SearchResult>> StructuredStringSearch(string k1, string k2, string k3, string k4);

    Task<List<BestMatch>> BestMatch(string[] keywrods);

    Task<List<SearchResult>> ExactMatch(string[] keywords);

    Task<List<SearchResult>> SimilarTitles(string keyword);

}
