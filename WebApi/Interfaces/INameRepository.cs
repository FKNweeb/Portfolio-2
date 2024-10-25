using WebApi.Models;

namespace WebApi.Interfaces;

public interface INameRepository
{
    Task<List<Name>> GetAllNamesAsync();
}
