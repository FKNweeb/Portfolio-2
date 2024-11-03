using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
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

        [HttpGet(Name =nameof(GetAllNames))]
        public async Task<IActionResult> GetAllNames(int page=0, int pageSize=25)
        {
            var names = await _nameRepo.GetAllNamesAsync(page, pageSize);
            if(names == null) {return NotFound();}

            var total = _nameRepo.NumberOfName();

            object result = CreatePaging(nameof(GetAllNames), page, pageSize, total, names);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetNameById([FromRoute] string id )
        {
            var title = _context.Names.FirstOrDefault(t => t.NameId == id);
            if(title == null) return NotFound();

            return Ok(title);
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

        [HttpGet("crewcharacter", Name =(nameof(GetNameAndCrewCharacter)))]
        public async Task<IActionResult> GetNameAndCrewCharacter(int page = 0, int pageSize = 25){
            var kft = await _nameRepo.GetNameAndCrewCharacterAsync(page, pageSize);
            var total = _nameRepo.NumberOfName();

            object result = CreatePaging(
                nameof(GetNameAndCrewCharacter),
                page,
                pageSize,
                total,
                kft
            );
            return Ok(result);
        }

        [HttpGet("crew", Name = (nameof(GetNameAndCrew)))]
        public async Task<IActionResult> GetNameAndCrew(int page = 0, int pageSize = 25)
        {
            var kft = await _nameRepo.GetNameAndCrewAsync(page, pageSize);
            var total = _nameRepo.NumberOfName();

            object result = CreatePaging(
                nameof(GetNameAndCrew),
                page,
                pageSize,
                total,
                kft
            );
            return Ok(result);
        }

        [HttpGet("crew/jobs", Name = (nameof(GetNameAndCrewJobs)))]
        public async Task<IActionResult> GetNameAndCrewJobs(int page = 0, int pageSize = 25)
        {
            var kft = await _nameRepo.GetNameAndJobAsync(page, pageSize);
            var total = _nameRepo.NumberOfName();

            object result = CreatePaging(
                nameof(GetNameAndCrewJobs),
                page,
                pageSize,
                total,
                kft
            );
            return Ok(result);
        }

        [HttpGet("crew/categories", Name = (nameof(GetNameAndCategory)))]
        public async Task<IActionResult> GetNameAndCategory(int page = 0, int pageSize = 25)
        {
            var kft = await _nameRepo.GetNameAndCategoryAsync(page, pageSize);
            var total = _nameRepo.NumberOfName();

            object result = CreatePaging(
                nameof(GetNameAndCategory),
                page,
                pageSize,
                total,
                kft
            );
            return Ok(result);
        }
    }
}
