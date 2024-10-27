using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.TitleRelatedModels;
using WebApi.DTO.TitleDtos;

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
            .Skip(page)
            .Take(pageSize)
            .ToListAsync();
    }
}
