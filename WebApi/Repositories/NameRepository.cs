using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.NameRelatedModels;

namespace WebApi.Repositories;

public class NameRepository : INameRepository
{
    private readonly ImdbContext _context;

    public NameRepository(ImdbContext context)
    {
        _context = context;
    }

    public async Task<List<Name>> GetAllNamesAsync()
    { 
        return await _context.Names.ToListAsync();
    }

    public async Task<List<Name>> GetAllKnownForTitle(int page, int pageSize){
        return await _context.Names
            .Include(n => n.KnownForTitles)
            .ThenInclude(n => n.Title)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    } 

    public async Task<List<Name>> GetNameAndProfessionAsync(int page, int pageSize){
        return await _context.Names
            .Include(n => n.ProfessionNames)
            .ThenInclude(n => n.Profession)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}
