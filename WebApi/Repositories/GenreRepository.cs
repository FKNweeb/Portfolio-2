using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using WebApi.Data;
using WebApi.DTO.TitleDtos;
using WebApi.Interfaces;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly ImdbContext _context;

    public GenreRepository(ImdbContext context)
    {
        _context = context; 
    }

    public async Task<List<GenreWithTitles>> GetAllTitlesByGenre(int page, int pageSize)
    {
        return await _context.Genres
            .Include(g => g.TitleGenres)
            .ThenInclude(tg => tg.Title)
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(g => new GenreWithTitles
            {
                GenreName = g.GenreName,
                Titles = g.TitleGenres.Select(tg => tg.Title).ToList()
            })
            .ToListAsync();
    }

    public async Task<List<GenreWithTitles>> GetTitlesBySpecificGenre(string genre, int page,int pageSize)
    {
        return await _context.Genres
            .Where(e=>e.GenreName == genre)
            .Include(g => g.TitleGenres)
                .ThenInclude(tg => tg.Title)
            .Skip(page * pageSize)
            .Take(pageSize)
            .Select(g => new GenreWithTitles
            {
                GenreName = g.GenreName,
                Titles = g.TitleGenres.Select(tg => tg.Title).ToList()
            })
            .ToListAsync();
    }

    public int NumberOfGenre()
    {
        return _context.Genres.Count();
    }
}
