using WebApi.Models.FunctionBasedModels;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO.TitleDtos;
using WebApi.Interfaces;
using WebApi.Mappers;


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


    [HttpGet("similartitles/{id}")]
    public async Task<IActionResult> SimilarTitles([FromRoute] string id)
    {
        var SimilarTitles = await _titleRepo.SimilarTitles(id);
        if (SimilarTitles == null)
        {
            return NotFound();
        }
        return Ok(SimilarTitles);
    }

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

                    object result = CreatePaging(
                    nameof(Search),
                    page,
                    pageSize,
                    total,
                    results
                    );

                    if( results == null)
                    {
                        return NotFound();
                    }

                    if (results.Count == 1)
                    {
                        var id = results[0].tconst;
                        var title = await _titleRepo.GetTitleById(id);
                        var titleDto = title.ToTitleAndPlotDto();
                        return Ok(titleDto);
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


    [HttpGet("genre", Name =nameof(GetTitlesByGenre))]
    public async Task<IActionResult> GetTitlesByGenre(int page = 0, int pageSize = 25)
    {
        var titles = await _genreRepository.GetAllTitlesByGenre(page, pageSize);
        if (titles == null)
        {
            return NotFound();
        }
        var total =  _genreRepository.NumberOfTitles();

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

    [HttpGet("genre/{genre}")]
    public async Task<IActionResult> GetTitlesForGenra([FromRoute] string genre, int page=0, int pageSize = 25)
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
    
    //TODO: Datavalidation
    //TODO: We could also implement for every endpoint sorting 
    //TODO: We should implement paging for every endpoint 
}
