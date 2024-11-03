using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using WebApi.Data;
using WebApi.DTO.NameDtos;
using WebApi.Interfaces;
using WebApi.Mappers;


namespace WebApi.Controllers;

[Route("api/names")]
[ApiController]
public class NameController : BaseController
{
    private readonly ImdbContext _context;
    private readonly INameRepository _nameRepo;
    private readonly LinkGenerator _linkGenerator;

    public NameController(INameRepository nameRepo, LinkGenerator linkGenerator) : base(linkGenerator)
    {
        _nameRepo = nameRepo;
        _linkGenerator = linkGenerator;
    }

    [HttpGet(Name =nameof(GetAllNames))]
    public async Task<IActionResult> GetAllNames([FromQuery] QueryName query, int page=0, int pageSize=25)
    {
        var names = await _nameRepo.GetAllNamesAsync(query, page, pageSize);
        if(names == null) {return NotFound();}

        var total = _nameRepo.NumberOfName();

        object result = CreatePaging(nameof(GetAllNames), page, pageSize, total, names);

        return Ok(result);
    }

    [HttpGet("{primaryName}")]
    public async Task<IActionResult> GetName([FromRoute] string primaryName)
    {
        var name = await _nameRepo.GetNameByPrimaryName(primaryName);
        if(name == null)
        {
            return NotFound();
        }
        return Ok(name);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchForName([FromQuery] SearchNameQuery query)
    {
        var names = await _nameRepo.SearchForName(query.title, query.plot, query.character, query.person);
        if (names == null) { return NotFound(); }

        return Ok(names);
    }

    [HttpGet("searchcoplayer/{nameId}")]
    public async Task<IActionResult> SearForCoPlayers([FromRoute] string nameId)
    {
        var names = await _nameRepo.FindCoPlayers(nameId);
        if (names == null) { return NotFound(); }
        return Ok(names);
    }

    [HttpGet("titlerelated/{name}")]
    public async Task<IActionResult> TitleRelatedName([FromRoute] string name)
    {
        var names = await _nameRepo.FindTitlesRelatedWithName(name);
        if(names == null) { return NotFound(); }
        return Ok(names);
    }

    //TODO: Reconfigure the endpoints
}
