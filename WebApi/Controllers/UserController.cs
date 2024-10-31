using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using WebApi.DTO.TitleDtos;
using WebApi.DTO.UserDtos;
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

        var usersDto = users.Select(s => s.ToUserDto());
        
        return Ok(usersDto);
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        var user = await _userRepo.GetUserById(id);
        if(user == null) return NotFound("User does not exist");
        var userDto = user.ToUserDto();

        return Ok(userDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto CreateUserDto)
    {
        var user = CreateUserDto.FromCreateUserDtoToUser();
        await _userRepo.CreateUser(user);

        return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user.ToUserDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var user = await _userRepo.DeleteUser(id);
        if (user == null) return NotFound();
        var userDto = user.ToUserDto();
        return Ok($"User: {userDto.ToString()} has been deleted");
    }

    [HttpGet("history")]
    public async Task<IActionResult> GetUsersHistory()
    {
        var history = await _userRepo.GetHistory();
        if (history == null) return NotFound();
        return Ok(history);
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

    [HttpGet("bookmark/{id}")]
    public async Task<IActionResult> GetBookMarkByUser(int id)
    {
        var bookmark = await _userRepo.GetUsersBookmarksForName(id);
        if (bookmark == null) return NotFound();    
        return Ok(bookmark);
    }


    //TODO: All the Crud Operations ( Create User, Read User, Put User, Delete User)
}

