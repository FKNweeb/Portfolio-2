using WebApi.Models.FunctionBasedModels;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.TitleDtos;
using WebApi.Interfaces;
using WebApi.Mappers;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models.TitleRelatedModels;


namespace WebApi.Controllers;

[ApiController]
[Route("api/titles")]
public class TitleController : BaseController
{
    private readonly ITitlteRepository _titleRepo;
    private readonly LinkGenerator _linkGenerator;
    private readonly IGenreRepository _genreRepository;


    public TitleController(ITitlteRepository titleRepo, LinkGenerator linkGenerator, IGenreRepository genreRepository) : base(linkGenerator)
    {
        _titleRepo = titleRepo;
        _linkGenerator = linkGenerator;
        _genreRepository = genreRepository;
    }

    /// <summary>
    /// Returns all titles found in the database  paginated.
    /// </summary>
    /// <param name="page">Number of Page</param>
    /// <param name="pageSize">Items per page.</param>
    /// <returns>Return an Ok response to the user, as well as paginated list of TItleAndPlotDto</returns>
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

    /// <summary>
    /// Returns titles and their rating.
    /// </summary>
    /// <param name="queryRating">Provides the desired actions from the user. Such as the rate they are looking for or the order of the results.</param>
    /// <param name="page">Number of Page</param>
    /// <param name="pageSize">Items per page.></param>
    /// <returns>Returns ok response or not found, as well as paginated list of TitleAndRating.</returns>

    [HttpGet("rate", Name =nameof(GetTitlesByRate))]
    public async Task<IActionResult> GetTitlesByRate([FromQuery] QueryRating queryRating, int page=0, int pageSize=25)
    {
        var titles = await _titleRepo.GetTitleAndRating(queryRating, page, pageSize);

        if(titles == null)
        {
            return NotFound();
        }
        var total =  _titleRepo.NumberOfTitles();

        var titlesDto = titles.Select(e => e.ToTitleAndRate());

        object result = CreatePaging(
            nameof(GetTitlesByRate),
            page,
            pageSize,
            total,
            titlesDto
            );
        return Ok(result);
    }


    /// <summary>
    /// Returns similar titles to the title id provided based on their genres.
    /// </summary>
    /// <param name="id">Id is the titleId for which the users is looking for similar titles.</param>
    /// <returns>Returns ok reposnse or not found response, with a list of all titles that match the genre.</returns>
    [HttpGet("similartitles/{id}")]
    public async Task<IActionResult> SimilarTitles([FromRoute] string id)
    {
        var SimilarTitles = await _titleRepo.SimilarTitles(id);
        if (SimilarTitles.IsNullOrEmpty())
        {
            return NotFound();
        }
        return Ok(SimilarTitles);
    }


    /// <summary>
    /// Combined search methods for one or multiple keywords.
    /// </summary>
    /// <param name="query">Provides the desired actions from the user. Such as the keywords they are using in order to search and the search method 
    /// they would like to use.</param>
    /// <param name="page">Number of Page</param>
    /// <param name="pageSize">Items per page.</param>
    /// <returns>Ok response if the search had a result or NotFound if the search did not have any match or bad request if the users has provided more keywords 
    /// than allowed.</returns>
    [HttpGet("search", Name = nameof(Search))]
    public async Task<IActionResult> Search([FromQuery] QueryObject query, int page=0, int pageSize=25)
    {
        if(query.keywords == null || !query.keywords.Any())
        {
            return BadRequest("Keywords are required.");
        }

        List<SearchResult> results = null;

        switch(query.SearchType?.ToLower())
        {
            case "bestmatch":

                var titlesBeestMacth = await _titleRepo.BestMatch(query.keywords.ToArray());
                var Searchresult = titlesBeestMacth.Select(e => e.ToSearchResultFromBestMatch());
                results = Searchresult.ToList();
                break;
            case "exactmatch":
                results = await _titleRepo.ExactMatch(query.keywords.ToArray());
                break;
            case "structured":
                if(query.keywords.Count > 4)
                {
                    return BadRequest("Structured search support up to 4 keywords.");
                }
                results = await _titleRepo.StructuredStringSearch(query.keywords[0], query.keywords[1]
                    , query.keywords[2], query.keywords[3]);
                break;
            default:
                if(query.keywords.Count == 1)
                {
                    results = await _titleRepo.SearchWithKeyword(query.keywords[0], page, pageSize);

                    var total = _titleRepo.NumberOfTitlesPerKeyword(query.keywords[0]);

                    var titles = new List<Title>();
                    foreach(var tconst in results)
                    {
                        titles.Add(await _titleRepo.GetTitleById(tconst.tconst));
                    }
                    var titlesDto = titles.Select(t => t.ToTitleAndPlotDto());



                    object result = CreatePaging(
                    nameof(Search),
                    page,
                    pageSize,
                    total,
                    titlesDto
                    );

                    if( results == null)
                    {
                        return NotFound();
                    }
                    return Ok(result);
                }
                break;
        }

        if(results == null || !results.Any())
        {
            return NotFound();
        }

        return Ok(results);
    }


    /// <summary>
    /// Returns Titles that falls under the same category.
    /// </summary>
    /// <param name="page">Number of Page</param>
    /// <param name="pageSize">Items per page.</param>
    /// <returns>Returns Ok response, the name of the genre and all titles with primary name and title id that falls under that genre.</returns>

    [HttpGet("genre", Name =nameof(GetTitlesByGenre))]
    public async Task<IActionResult> GetTitlesByGenre(int page = 0, int pageSize = 25)
    {
        var titles = await _genreRepository.GetAllTitlesByGenre(page, pageSize);
        if (titles == null)
        {
            return NotFound();
        }
        var total =  _genreRepository.NumberOfGenre();

        var titlesDto = titles.Select(t => new
        {
            t.GenreName,
            Titles = t.Titles.Select(e => e.ToTitleAndGenre()).ToList()
        }).ToList();

        object result = CreatePaging(
           nameof(GetTitlesByGenre),
           page,
           pageSize,
           total,
           titlesDto
           );
        return Ok(result);
    }


    /// <summary>
    /// Returns titles that falls under a specific genre.
    /// </summary>
    /// <param name="genre">The specific genre</param>
    /// <param name="page">Number of Page</param>
    /// <param name="pageSize">Items per Page</param>
    /// <returns>OK Response or NotFound, and the primary title.</returns>
    [HttpGet("genre/{genre}")]
    public async Task<IActionResult> GetTitlesForGenre([FromRoute] string genre, int page=0, int pageSize=25)
    {
        var titles = await _genreRepository.GetTitlesBySpecificGenre(genre , page, pageSize);

        var titlesDto = titles.Select(t => new
        {
            t.GenreName,
            Titles = t.Titles.Select(e => e.ToTitleAndGenre()).ToList()
        }).ToList();

        if(titles == null)
        {
            return NotFound();
        }
        
        
        return Ok(titlesDto);
        
    }
    
}
