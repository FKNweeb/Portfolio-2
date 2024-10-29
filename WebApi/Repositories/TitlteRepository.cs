using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.TitleRelatedModels;
using WebApi.DTO.TitleDtos;
using WebApi.Models;

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
            .Include(d=>d.TitleDate)
            .Where(t => t.TitlePlot != null)
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

    public async Task<List<Title>> GetTilteByLanguage(int page, int pageSize)
    {
        //return await  _context.Titles
        //    .Include(t => t.TitleKnownAs)
        //    .Skip(page * pageSize)
        //    .Take(pageSize)
        //    .ToListAsync();


        return await _context.Titles
        .Select(t => new Title
        {
            TitleId = t.TitleId,
            PrimaryTitle = t.PrimaryTitle,
            OriginalTitle = t.OriginalTitle,
            RuntimeMinutes = t.RuntimeMinutes,
            AverageRating = t.AverageRating,
            NumberOfVotes = t.NumberOfVotes,
            TitleKnownAs = t.TitleKnownAs
                .Select(tka=> new TitleKnownAs
                {
                    TitleId = tka.TitleId,
                    OrderingAkas = tka.OrderingAkas,
                    KnownAsTitle = tka.KnownAsTitle,
                    Type = tka.Type,
                    Language = tka.Language,
                    Region = tka.Region
                   
                })

                .ToList()
        })
        .Skip(page * pageSize)
        .Take(pageSize)
        .ToListAsync();

    }
}
