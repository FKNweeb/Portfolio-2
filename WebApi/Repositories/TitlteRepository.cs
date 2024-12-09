using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.TitleRelatedModels;
using WebApi.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using WebApi.Models.FunctionBasedModels;
using WebApi.DTO.TitleDtos;
using WebApi.Models.NameRelatedModels;

namespace WebApi.Repositories;

public class TitleRepository : ITitlteRepository
{
    private readonly ImdbContext _context;

    public TitleRepository(ImdbContext context)
    {
        _context = context;
    }

    //Add ordering for sake of the frontend results
    public async Task<List<Title>> GetAllAsync(int page, int pageSize)
    {
        return await _context.Titles
            .Include(t => t.TitlePlot)
            .Include(d => d.TitleDate)
            .Include(g => g.TitleGenres)
                .ThenInclude(gn => gn.Genre)
            .Include(a => a.TitleKnownAs)
                .ThenInclude(l => l.Language)
            .Include(p => p.TitlePoster)
            .Include(tt => tt.TitleIsType)
            .OrderBy(tt=>tt.TitleIsType.TypeOfTitle == "movie" ? 0 : 1) //Ordering prioritize movie
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    } 


    public int NumberOfTitles()
    {
        return _context.Titles.Count();
    }

    public async Task<Title?> GetTitleById(string id)
    {

        return await _context.Titles
        .Where(t => t.TitleId == id)
        .Include(t => t.TitlePlot)
        .Include(d => d.TitleDate)
        .Include(g => g.TitleGenres)
            .ThenInclude(gn => gn.Genre)
        .Include(a => a.TitleKnownAs)
            .ThenInclude(l => l.Language)
        .Include(p => p.TitlePoster)
        .Include(tt => tt.TitleIsType)
        .Include(c => c.Crews)
        .ThenInclude(cn => cn.Name)
        .Select(t => new Title
        {
            TitleId = t.TitleId,
            PrimaryTitle = t.PrimaryTitle,
            TitlePlot = t.TitlePlot,
            TitleDate = t.TitleDate,
            TitleGenres = t.TitleGenres.Select(gn => new TitleGenre
            {
                Genre = gn.Genre
            }).ToList(),
            TitleKnownAs = t.TitleKnownAs.Select(l => new TitleKnownAs
            {
                Language = l.Language
            }).ToList(),
            TitlePoster = t.TitlePoster,
            TitleIsType = t.TitleIsType,
            Crews = t.Crews.Select(n => new Crew
            {
                Name = n.Name
            }).ToList(),
            AverageRating = t.AverageRating,
            NumberOfVotes = t.NumberOfVotes
        })
        .FirstOrDefaultAsync();

    }


    public async Task<List<SearchResult>> SearchWithKeyword(string keyword, int page, int pageSize)
    {
        return await _context.StringSearch(keyword)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public int NumberOfTitlesPerKeyword(string keyword)
    {
        return _context.StringSearch(keyword).Count();
    }


    public async Task<List<SearchResult>> StructuredStringSearch(string k1,string k2, string k3, string k4)
    {
        return await _context.StructuredStringSearch(k1,k2,k3,k4)
            .ToListAsync();
    }

    public async Task<List<BestMatch>> BestMatch(string[] keywrods)
    {
        return await _context.BestMatch(keywrods).ToListAsync();
    }

    public async Task<List<SearchResult>> ExactMatch(string[] keywords)
    {
        return await _context.ExactMatch(keywords).ToListAsync();
    }

    public async Task<List<SearchResult>> SimilarTitles(string keyword)
    {
        return await _context.SimilarTitles(keyword).ToListAsync();
    }

    public async Task<List<Title>> GetTitleAndRating(QueryRating queryObject, int page, int pageSize)
    {
        var titles =  _context.Titles.Where(e=>e.AverageRating != null && e.NumberOfVotes != null).AsQueryable();

        if (queryObject.Rate != null)
        {
            titles = titles.Where(r=>r.AverageRating == queryObject.Rate);
        }

        if(!string.IsNullOrWhiteSpace(queryObject.SortBy))
        {
            if(queryObject.SortBy == "rate")
            {
               titles = queryObject.IsDescending ? titles.OrderByDescending(r => r.AverageRating) : titles.OrderBy(r=>r.AverageRating);
            }
        }

        return await titles.Skip(page * pageSize).Take(pageSize).ToListAsync();
    }
}
