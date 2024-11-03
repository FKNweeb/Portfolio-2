using WebApi.DTO.NameDtos;
using WebApi.Models.NameRelatedModels;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Interfaces;

public interface INameRepository
{
    Task<List<GetAllNameDTO>> GetAllNamesAsync(QueryName query, int page, int pageSize);
    Task<List<Name>> GetAllKnownForTitle(int page, int pageSize);
    Task<List<Name>> GetNameAndProfessionAsync(int page, int pageSize);
    Task<List<Name>> GetNameAndCrewCharacterAsync(int page, int pageSize);
    Task<List<Name>> GetNameAndCrewAsync(int page, int pageSize);
    Task<List<Crew>> GetNameAndJobAsync(int page, int pageSize);
    Task<List<Name>> GetNameAndCategoryAsync(int page, int pageSize);

    public int NumberOfName();
}
