using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using WebApi.DTO.TitleDtos;
using WebApi.Interfaces;
using WebApi.Mappers;
using WebApi.Models.TitleRelatedModels;

namespace WebApi.Controller;

[ApiController]
[Route("api/user")]
public class UserController : BaseController
{
    private readonly IUserRepository _UserRepo;
    private readonly LinkGenerator _linkGenerator;

    public UserController(IUserRepository UserRepo, LinkGenerator linkGenerator) : base(linkGenerator)
    {
        _UserRepo = UserRepo;
        _linkGenerator = linkGenerator;
    }


    [HttpGet(Name = nameof(GetAllUsers))]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _UserRepo.GetAllAsync();
        if (users == null) return NotFound();
      
        return Ok(users);
    }


}

