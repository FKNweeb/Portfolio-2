using WebApi.Models.NameRelatedModels;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Interfaces;

public interface INameRepository
{
    Task<List<Name>> GetAllNamesAsync();
    Task<List<Name>> GetAllKnownForTitle(int page, int pageSize);
    Task<List<Name>> GetNameAndProfessionAsync(int page, int pageSize);
    Task<List<Name>> GetNameAndCrewCharacterAsync(int page, int pageSize);
    Task<List<Name>> GetNameAndCrewAsync(int page, int pageSize);
    Task<List<Crew>> GetNameAndJobAsync(int page, int pageSize);
    public int NumberOfName();
}
