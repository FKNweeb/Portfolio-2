using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using WebApi.DTO.TitleDtos;
using WebApi.Interfaces;
using WebApi.Mappers;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Controller;

[ApiController]
[Route("api/users")]
public class UserController : BaseController
{
    private readonly IUserRepository _userRepo;
    private readonly LinkGenerator _linkGenerator;

    public UserController(IUserRepository UserRepo, LinkGenerator linkGenerator) : base(linkGenerator)
    {
        _userRepo = UserRepo;
        _linkGenerator = linkGenerator;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepo.GetAllAsync();
        if (users == null) return NotFound();
      
        return Ok(users);
    }

    [HttpGet("ratename", Name = (nameof(GetRateNameAndName)))]
        public async Task<IActionResult> GetRateNameAndName(int page = 0, int pageSize = 25)
        {
            var kft = await _userRepo.GetRateNameAndNameAsync(page, pageSize);
            var total = _userRepo.NumberOfUsers();

            object result = CreatePaging(
                nameof(GetRateNameAndName),
                page,
                pageSize,
                total,
                kft
            );
            return Ok(result);
        }

}

