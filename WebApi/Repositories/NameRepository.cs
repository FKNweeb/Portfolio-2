using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Models.TitleRelatedModels;

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
}
