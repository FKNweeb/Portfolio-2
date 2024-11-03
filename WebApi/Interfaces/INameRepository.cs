using WebApi.DTO.NameDtos;
using WebApi.DTO.UserDtos;
using WebApi.Models.FunctionBasedModels;
using WebApi.Models.NameRelatedModels;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Interfaces;

public interface INameRepository
{
    Task<List<GetAllNameDTO>> GetAllNamesAsync(QueryName query, int page, int pageSize);

    Task<GetAllNameDTO?> GetNameByPrimaryName(string PrimaryName);

    Task<List<NameSearchResults>> SearchForName(string title, string plot, string character, string person);

    Task<List<FindCoPlayersResults>> FindCoPlayers(string nconst);

    Task<TitleRelatedName?> FindTitlesRelatedWithName(string name);
    
    
    
    

    public int NumberOfName();
}
