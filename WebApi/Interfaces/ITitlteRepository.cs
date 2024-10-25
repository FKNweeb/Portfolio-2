using DataLayer.Models;

namespace WebApi.Interfaces;

public interface ITitlteRepository
{
    Task<List<Title>> GetAllAsync();
}
