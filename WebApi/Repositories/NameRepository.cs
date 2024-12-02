using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.Data;
using WebApi.DTO.NameDtos;
using WebApi.DTO.UserDtos;
using WebApi.Interfaces;
using WebApi.Models.FunctionBasedModels;
using WebApi.Models.NameRelatedModels;

namespace WebApi.Repositories;

public class NameRepository : INameRepository
{
    private readonly ImdbContext _context;

    public NameRepository(ImdbContext context)
    {
        _context = context;
    }

    public async Task<List<GetAllNameDTO>> GetAllNamesAsync(QueryName query,int page, int pageSize)
    {

        var queryable = _context.Names
             .Include(tk => tk.KnownForTitles)
             .ThenInclude(t => t.Title)
             .Include(p => p.ProfessionNames)
             .ThenInclude(p => p.Profession)
             .AsQueryable();

        if(!string.IsNullOrWhiteSpace(query.SortBy))
        {
            if(query.SortBy == "name")
            {
                queryable = query.IsDescending ? queryable.OrderByDescending(n=>n.PrimaryName)
                        : queryable.OrderBy(n => n.PrimaryName);
            }
        }

        var names = await queryable
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(n=> new GetAllNameDTO
            {
                Name = n.PrimaryName,
                BirthYear = n.BirthYear,
                DeathYear = n.DeathYear,
                KnownForTitles = n.KnownForTitles.Select(g => g.Title.PrimaryTitle).ToList(),
                Professions = n.ProfessionNames.Select(p => p.Profession.ProfessionTitle).ToList(),
            }).ToListAsync();

        return names;
    }


    public int NumberOfName()
    {
        return _context.Names.Count();
    }

    public async Task<List<GetAllNameDTO?>> GetNameByPrimaryName(string PrimaryName, int page, int pageSize)
    {
        return await _context.Names
            .Where(n=>n.PrimaryName.ToLower().Contains(PrimaryName.ToLower()))
            .Include(tk => tk.KnownForTitles)
            .ThenInclude(t => t.Title)
            .Include(p => p.ProfessionNames)
            .ThenInclude(p => p.Profession)
            .Skip(page * pageSize)
            .Take(pageSize )
             .Select(n => new GetAllNameDTO
             {
                 
                 Name = n.PrimaryName,
                 NameId = n.NameId,
                 BirthYear = n.BirthYear,
                 DeathYear = n.DeathYear,
                 KnownForTitles = n.KnownForTitles.Select(g => g.Title.PrimaryTitle).ToList(),
                 Professions = n.ProfessionNames.Select(p => p.Profession.ProfessionTitle).ToList(),
             }).ToListAsync();
    }

    public async Task<List<NameSearchResults>> SearchForName(string title, string plot, string character, string person)
    {
        return await _context.StructuredNameSearch(title, plot, character, person).ToListAsync();
    }

    public async Task<List<FindCoPlayersResults>> FindCoPlayers(string nconst)
    {
       return await _context.FindCoPlayers(nconst).ToListAsync();
    }

    public async Task<TitleRelatedName?> FindTitlesRelatedWithName(string name)
    {
        return await  _context.Names
            .Where(w=>w.PrimaryName == name)
            .Include(c => c.CrewCharacters)
            .ThenInclude(t => t.Title)
            .Include(c=>c.Crews)
            .ThenInclude(c=>c.CrewJob)
            .ThenInclude(j=>j.Job)
            .Select(n=> new TitleRelatedName
            {
                Name = n.PrimaryName,
                Title = n.CrewCharacters.Select(c => c.Title.PrimaryTitle).Distinct().ToList(),
                CrewsCharacter = n.CrewCharacters.Select(c=>c.CharacterDescription).Distinct().ToList(),
                Crews = n.Crews.Select(c=>c.Category.CategoryName).Distinct().ToList(),
               

            })
            .FirstOrDefaultAsync();
            
    }
}
