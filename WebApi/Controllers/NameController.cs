using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Mappers;


namespace WebApi.Controllers
{
    [Route("api/names")]
    [ApiController]
    public class NameController : BaseController
    {
        private readonly ImdbContext _context;
        private readonly INameRepository _nameRepo;
        private readonly LinkGenerator _linkGenerator;

        public NameController(ImdbContext imdb, INameRepository nameRepo, LinkGenerator linkGenerator) : base(linkGenerator)
        {
            _context = imdb;
            _nameRepo = nameRepo;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNames()
        {
            var titles = await _nameRepo.GetAllNamesAsync();
            var titlesDto = titles.Select(s => s.ToGetAllNameDTO());

            return Ok(titlesDto);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetNameById([FromRoute] string id )
        {
            var title = _context.Names.FirstOrDefault(t => t.NameId == id);
            if(title == null) return NotFound();

            return Ok(title);
        }
        [HttpGet("KnownForTitle", Name =(nameof(GetAllKnownForTitle)))]
        public async Task<IActionResult> GetAllKnownForTitle(int page = 0, int pageSize = 25){
            var kft = await _nameRepo.GetAllKnownForTitle(page, pageSize);
            var total = _context.Names.Count();
            object result = CreatePaging(
                nameof(GetAllKnownForTitle),
                page,
                pageSize,
                total,
                kft
            );
            return Ok(result);
        }

        [HttpGet("profession", Name =(nameof(GetNameAndProfession)))]
        public async Task<IActionResult> GetNameAndProfession(int page = 0, int pageSize = 25){
            var kft = await _nameRepo.GetNameAndProfessionAsync(page, pageSize);
            var total = _nameRepo.NumberOfName();
            
            object result = CreatePaging(
                nameof(GetNameAndProfession),
                page,
                pageSize,
                total,
                kft
            );
            return Ok(result);
        }
    }
}
