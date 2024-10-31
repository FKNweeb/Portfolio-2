using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.TitleRelatedModels;
using WebApi.DTO.TitleDtos;
using WebApi.Models;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

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
    /// Retrives titles that match the start year
    /// </summary>
    /// <param name="startyear"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<List<Title>> GetAllTitlesByDate(string startyear, int page, int pageSize)
    {
        return await _context.Titles
            .Include(t=> t.TitleDate)
            .Where(t=> t.TitleDate.StartYear == startyear)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Title>> GetAllTitlesByType(int page, int pageSize)
    {
        return await _context.Titles
            .Include(t=> t.TitleIsType)
            .ThenInclude(t=> t.TitleType)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Title>> GetAllTitlesWithPoster(int page, int pageSize)
    {
        return await _context.Titles
            .Include(t=>t.TitlePoster)
            .Where(t=>t.TitlePoster != null)
            .Skip(page * pageSize)
            .Take (pageSize)
            .ToListAsync();
    }

    public async Task<List<Title>> GetTitleAndWordIndex(string title, int page, int pageSize)
    {

        return await _context.Titles
            .Where (t=>t.WordIndexes.Any() && t.TitleId == title)
            .Include(t=>t.WordIndexes)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<TitleKnownAs>> GetTilteByLanguage(int page, int pageSize)
    {


        return await _context.KnowAs
            .Include(t => t.Title)
            .Include(t => t.Language)
            .Include(t => t.Region)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();


        //    return await _context.Titles
        //    .Select(t => new Title
        //    {
        //        TitleId = t.TitleId,
        //        PrimaryTitle = t.PrimaryTitle,
        //        OriginalTitle = t.OriginalTitle,
        //        RuntimeMinutes = t.RuntimeMinutes,
        //        AverageRating = t.AverageRating,
        //        NumberOfVotes = t.NumberOfVotes,
        //        TitleKnownAs = t.TitleKnownAs
        //            .Select(tka => new TitleKnownAs
        //            {
        //                TitleId = tka.TitleId,
        //                OrderingAkas = tka.OrderingAkas,
        //                KnownAsTitle = tka.KnownAsTitle,
        //                Type = tka.Type,
        //                Language = tka.Language,
        //                Region = tka.Region

        //            })

        //            .ToList()
        //    })
        //    .Skip(page * pageSize)
        //    .Take(pageSize)
        //    .ToListAsync();

        //}
    }

    
    public async Task<List<Episode>> GetEpisodesByParentTitel(string id, int page, int pageSize)
    {
       return await _context.Episodes
            .Include(t=>t.Title)
            .Where(t=>t.ParentTitle == id)
            .Skip (page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Title>> GetRateTitle(string id, int page, int pageSize)
    {
       return await _context.Titles
            .Include(t => t.RateTitles)
            .ThenInclude(rt => rt.LocalTitleRating)
            .Where(t => t.TitleId == id)
            .Skip (page * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }


    /// <summary>
    /// Takes a string as parameter and returns the titleId and the primary title of titles that the string matches either their primary title or the plot
    /// </summary>
    /// <param name="keyword1"></param>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<List<SearchResult>> SearchWithKeyWords(string keyword1, int page, int pageSize)
    {
        var query = "SELECT * FROM string_search(@p0)";
        return await  _context.SearchResults
            .FromSqlRaw(query, keyword1)
            .Skip(page * pageSize)
            .Take (pageSize)
            .ToListAsync();
    }

    public int NumbErOfTitlesPerKeyWord(string keyword1)
    {
        var query = "SELECT * FROM string_search(@p0)";
        return _context.SearchResults
            .FromSqlRaw(query, keyword1)
            .Count();
    }
}
