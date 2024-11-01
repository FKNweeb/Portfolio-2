using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTO.TitleDtos;
using WebApi.Interfaces;
using WebApi.Mappers;
using WebApi.Models.FunctionBasedModels;


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

    //TODO: GetTitleById , ToTitleAndPlot
    //TODO: SearchFunctioncs that are relevant from the database

    

    [HttpGet("search/{keyword}", Name =nameof(SearchWithKeyword))]
    public async Task<IActionResult> SearchWithKeyword(string keyword, int page=0, int pageSize=25)
    {
        var title = await _titleRepo.SearchWithKeyword(keyword, page, pageSize);

        var total = _titleRepo.NumberOfTitlesPerKeyword(keyword);
        object result = CreatePaging(
           nameof(SearchWithKeyword),
           page,
           pageSize,
           total,
           title
           );
        return Ok(result);
    }

    [HttpGet("searchWithKeywords", Name = nameof(SearchWithStructuredKeywords))]
    public async Task<IActionResult> SearchWithStructuredKeywords(string k1, string k2, string k3, string k4)
    {
        var titles = await _titleRepo.StructuredStringSearch(k1, k2, k3, k4);
       
        return Ok(titles);
    }


    [HttpGet("bestmatch")]
    public async Task<IActionResult> BestMacth([FromQuery] QueryObject query)
    {        
        var BestMatchTitles = await _titleRepo.BestMatch(query.keywords.ToArray());
        if(BestMatchTitles == null)
        {
            return NotFound();
        }
        return Ok(BestMatchTitles);
    }
}
