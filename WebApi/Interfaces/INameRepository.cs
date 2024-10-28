using WebApi.Models;

namespace WebApi.Interfaces;

public interface INameRepository
{
    Task<List<Name>> GetAllNamesAsync();
    Task<List<Name>> GetAllKnownForTitle(int page, int pageSize);
}
