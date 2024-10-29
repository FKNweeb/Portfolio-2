using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTO.TitleDtos;
using WebApi.Interfaces;
using WebApi.Mappers;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Controllers;

[ApiController]
[Route("api/titles")]
public class TitleController : BaseController
{
    private readonly ITitlteRepository _titleRepo;
    private readonly LinkGenerator _linkGenerator;

    public TitleController(ITitlteRepository titleRepo, LinkGenerator linkGenerator) : base(linkGenerator)
    {
        _titleRepo = titleRepo;
        _linkGenerator = linkGenerator;

    }


    [HttpGet(Name = nameof(GetAllTitles))]
    public async Task<IActionResult> GetAllTitles(int page = 0, int pageSize = 25)
    {
        var titles = await _titleRepo.GetAllAsync(page, pageSize);
        var total = _titleRepo.NumberOfTitles();
        var titlesDto = titles.Select(s => s.ToTitleAndPlotDto());


        object result = CreatePaging(
            nameof(GetAllTitles),
            page,
            pageSize,
            total,
            titlesDto
            );
        return Ok(result);
    }


    [HttpGet("{startyear}", Name = nameof(GetAllTitlesByDate))]
    public async Task<ActionResult> GetAllTitlesByDate([FromRoute] string startyear, int page =0 , int pageSize=25)
    {
        var titles = await _titleRepo.GetAllTitlesByDate(startyear, page, pageSize);
        
        var total = _titleRepo.NumberOfTitles();
        
        var titlesDto = titles.Select(s=>s.ToTitleAndDateDto());

        
        return Ok(titlesDto);
    }

    [HttpGet("type", Name = nameof(GetTitlesByType))]
    public async Task<IActionResult> GetTitlesByType(int page=0, int pageSize=25)
    {
        var titles = await _titleRepo.GetAllTitlesByType(page, pageSize);

        var total = _titleRepo.NumberOfTitles();

        object result = CreatePaging(
            nameof(GetTitlesByType),
            page,
            pageSize,
            total,
            titles
            );
        return Ok(result);
    }

    [HttpGet("posters", Name =nameof(GetTitlesAndPosters))]
    public async Task<IActionResult> GetTitlesAndPosters(int page=0, int pageSize =25)
    {
        var titles = await _titleRepo.GetAllTitlesWithPoster(page, pageSize);
        var total = _titleRepo.NumberOfTitles();

        object result = CreatePaging(
            nameof(GetTitlesAndPosters),
            page,
            pageSize,
            total,
            titles
            );
        return Ok(result);
    }

    [HttpGet("wordindex/{id}", Name = nameof(GetWordIndex))]
    public async Task<IActionResult> GetWordIndex(string id, int page = 0, int pageSize = 25)
    {
        var titles = await _titleRepo.GetTitleAndWordIndex(id, page, pageSize);
        var total = _titleRepo.NumberOfTitles();

        object result = CreatePaging(
            nameof(GetWordIndex),
            page,
            pageSize,
            total,
            titles
            );
        return Ok(result);
    }

    [HttpGet("languages")]
    public async Task<IActionResult> GetTitlesByLnaguage(int page=0, int pageSize=25)
    {
        var titles = await _titleRepo.GetTilteByLanguage(page, pageSize);
        var total = _titleRepo.NumberOfTitles();
        
        object result = CreatePaging(
           nameof(GetTitlesByLnaguage),
           page,
           pageSize,
           total,
           titles
           );
        return Ok(result);

    }

}
