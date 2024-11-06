using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Tracing;
using System.Numerics;
using WebApi.Data;
using WebApi.DTO.NameDtos;
using WebApi.Interfaces;
using WebApi.Mappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace WebApi.Controllers;

[Route("api/names")]
[ApiController]
public class NameController : BaseController
{
    
    private readonly INameRepository _nameRepo;
    private readonly LinkGenerator _linkGenerator;
    private readonly IUserRepository _userRepository;

    public NameController(INameRepository nameRepo, LinkGenerator linkGenerator, IUserRepository userRepository) : base(linkGenerator)
    {
        _nameRepo = nameRepo;
        _linkGenerator = linkGenerator;
        _userRepository = userRepository;
    }
    /// <summary>
    /// Get all names in a paginated list, if they wants to order by name 
    /// in a ascending and descending way
    /// </summary>
    /// <param name="query">SortBy and IsDescending</param>
    /// <param name="page">The page number</param>
    /// <param name="pageSize">The size of he pages</param>
    /// <returns></returns>

    [HttpGet(Name =nameof(GetAllNames))]
    public async Task<IActionResult> GetAllNames([FromQuery] QueryName query, int page=0, int pageSize=25)
    {
        var names = await _nameRepo.GetAllNamesAsync(query, page, pageSize);
        
        if(names == null) {return NotFound();}

        var total = _nameRepo.NumberOfName();

        object result = CreatePaging(nameof(GetAllNames), page, pageSize, total, names);

        return Ok(result);
    }
    /// <summary>
    /// Finds information about a person by their primary name
    /// and updates search history
    /// </summary>
    /// <param name="primaryName">The name of a person</param>
    /// <returns></returns>

    [HttpGet("{primaryName}")]
    public async Task<IActionResult> GetName([FromRoute] string primaryName)
    {
        var name = await _nameRepo.GetNameByPrimaryName(primaryName);
        //Not a good practice because it violates restfull practices 


        //var updatedHistory = await _userRepository.UpdateSearchHistory(primaryName);
        if (name == null)
        {
            return NotFound();
        }
        return Ok(name);
    }
    /// <summary>
    /// Finds names based on differnet keywords, like title, plot, character.
    /// The search is saved in the search history
    /// </summary>
    /// <param name="query">The query contains title, plot, character and person</param>
    /// <returns></returns>

    [HttpGet("search")]
    public async Task<IActionResult> SearchForName([FromQuery] SearchNameQuery query)
    {
        var names = await _nameRepo.SearchForName(query.title, query.plot, query.character, query.person);
        var keywords = new List<string> { query.title, query.plot, query.character, query.person };
        
        //foreach (var keyword in keywords) 
        //{
        //    await _userRepository.UpdateSearchHistory(keyword);
        //}

        
        if (names == null) { return NotFound(); }

        return Ok(names);
    }
    /// <summary>
    /// Finds the coplayer/coplayers which are related to a specific person, by nameId
    /// </summary>
    /// <param name="nameId">The id of a person</param>
    /// <returns></returns>

    [HttpGet("searchcoplayer/{nameId}")]
    public async Task<IActionResult> SearForCoPlayers([FromRoute] string nameId)
    {
        var names = await _nameRepo.FindCoPlayers(nameId);
        if (names == null) { return NotFound(); }
        return Ok(names);
    }
    /// <summary>
    /// Findes all titls that related to a specific person, through the PrimaryName
    /// </summary>
    /// <param name="name">The name of a person</param>
    /// <returns></returns>
    [HttpGet("titlerelated/{name}")]
    public async Task<IActionResult> TitleRelatedName([FromRoute] string name)
    {
        var names = await _nameRepo.FindTitlesRelatedWithName(name);
        if(names == null) { return NotFound(); }
        return Ok(names);
    }

    //TODO: Reconfigure the endpoints
}
