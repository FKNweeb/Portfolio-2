using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.TitleRelatedModels;
using WebApi.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using WebApi.Models.FunctionBasedModels;

namespace WebApi.Repositories;

public class TitleRepository : ITitlteRepository
{
    private readonly ImdbContext _context;

    public TitleRepository(ImdbContext context)
    {
        _context = context;
    }


    /// <summary>
    /// Retrives a paginated ilst of titles that have a non-null plot.
    /// </summary>
    /// <param name="page">The number of the page to retrive</param>
    /// <param name="pageSize">The number of titles to retrive per page.</param>
    /// <returns>A task that represent the asynchronus operation. The 
    /// task result contains a list of objects.</returns>
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
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();

    } 

    public int NumberOfTitles()
    {
        return _context.Titles.Count();
    }


    /// <summary>
    /// Takes a string as parameter and returns the titleId and the primary title of titles that the string matches either their primary title or the plot
    /// </summary>
    /// <param name="keyword1"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
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




}
