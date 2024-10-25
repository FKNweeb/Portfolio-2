using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Mappers;


namespace WebApi.Controllers
{
    [Route("api/names")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly ImdbContext _context;
        private readonly INameRepository _nameRepo;

        public NameController(ImdbContext imdb, INameRepository nameRepo)
        {
            _context = imdb;
            _nameRepo = nameRepo;
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

    }
}
