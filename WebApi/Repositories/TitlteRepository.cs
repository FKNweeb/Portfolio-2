using DataLayer.Data;
using DataLayer.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;

namespace WebApi.Repositories;

public class TitleRepository : ITitlteRepository
{
    private readonly ImdbContext _context;

    public TitleRepository(ImdbContext context)
    {
        _context = context;
    }

    public async Task<List<Title>> GetAllAsync()
    { 
        return await _context.Titles.Include(a => a.TitleKnownAs).ToListAsync();
    }
}
