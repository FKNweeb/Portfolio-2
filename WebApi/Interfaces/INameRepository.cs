using WebApi.Models.NameRelatedModels;

namespace WebApi.Interfaces;

public interface INameRepository
{
    Task<List<Name>> GetAllNamesAsync();
    Task<List<Name>> GetAllKnownForTitle(int page, int pageSize);
    Task<List<Name>> GetNameAndProfessionAsync(int page, int pageSize);
}
