using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Mappers;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/titles")]
public class TitleController : ControllerBase
{
    private readonly ImdbContext _context;
    private readonly ITitlteRepository _titleRepo;

    public TitleController(ImdbContext imdb, ITitlteRepository titleRepo)
    {
        _context = imdb;
        _titleRepo = titleRepo;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllTitles()
    {
        var titles = await _titleRepo.GetAllAsync();
        var titlesDto = titles.Select(s => s.ToGetAllTitleDto());

        return Ok(titlesDto);
    }

    //[HttpGet("{id}")]
    //public IActionResult GetTitleById([FromRoute] string id )
    //{
    //    var title = _context.Titles.Include(k => k.TitleKnownAs).FirstOrDefault(t => t.TitleId == id);
    //    if(title == null) return NotFound();

    //    return Ok(title);
    //}

    [HttpGet]
    [Route("plots")]
    public async Task<IActionResult> GetTitleAndPlot()
    {
        var titles = await _context.TitlePlots.Include(t=> t.Title).ToListAsync();
            
        if (titles == null) { return NotFound(); }

        
        return  Ok(titles);


    }
}
