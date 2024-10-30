using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.NameRelatedModels;
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
            .Where(n=>n.KnownForTitles.Any())
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
    public async Task<List<Name>> GetNameAndCrewAsync(int page, int pageSize)
    {
        return await _context.Names
            .Include(n => n.Crews)
            .ThenInclude(c => c.Title)
            .Skip (page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Name>> GetNameAndCrewCharacterAsync(int page, int pageSize){
        return await _context.Names
            .Include(n => n.CrewCharacters)
            .ThenInclude(cc => cc.Title)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Crew>> GetNameAndJobAsync(int page, int pageSize){
        return await _context.Crews
            .Include(n => n.CrewJob)
            .ThenInclude(cj => cj.Job)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<List<Name>> GetNameAndCategoryAsync(int page, int pageSize){
        return await _context.Names
            .Include(n => n.Crews)
            .ThenInclude(c => c.Category)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public int NumberOfName()
    {
        return _context.Names.Count();
    }
}
