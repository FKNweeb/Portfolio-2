﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using WebApi.Controllers;
using WebApi.DTO.TitleDtos;
using WebApi.DTO.UserDtos;
using WebApi.Interfaces;
using WebApi.Mappers;
using WebApi.Models.TitleRelatedModels;
using WebApi.Services;

namespace WebApi.Controller;

[ApiController]
[Route("api/users")]
public class UserController : BaseController
{
    private readonly IUserRepository _userRepo;
    private readonly LinkGenerator _linkGenerator;
    private readonly IConfiguration _configuration;
    private readonly Hashing _hashing;

   
    public UserController(IUserRepository UserRepo, 
                          LinkGenerator linkGenerator,
                          IConfiguration configuration,
                          Hashing hashing) : base(linkGenerator)
    {
        _userRepo = UserRepo;
        _linkGenerator = linkGenerator;
        _configuration = configuration;
        _hashing = hashing;
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
        if(user == null) return NotFound();
        var userDto = user.ToUserDto();

        return Ok(userDto);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        if (await _userRepo.GetUserByUserName(createUserDto.UserName) != null){
            return BadRequest();
        }
        if (string.IsNullOrWhiteSpace(createUserDto.UserPassword)){
            return BadRequest();
        }
        (var hashedPwd, var salt) = _hashing.Hash(createUserDto.UserPassword);
        var user = createUserDto.FromCreateUserDtoToUser();
        user.UserPassword = hashedPwd;
        user.Salt = salt;
        await _userRepo.CreateUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user.ToUserDto());

    }

    [HttpPut("login")]
    public IActionResult Login(LoginUserDTO dto){
        var user = _userRepo.GetUserByUserName(dto.UserName);

        if (user == null) { return BadRequest(); }
        if (!_hashing.Verify(dto.Password, user.Result.UserPassword, user.Result.Salt)){ return BadRequest(); }

        var claims = new List<Claim> { 
            new Claim(ClaimTypes.Name, user.Result.UserName)
        };

        var secret = _configuration.GetSection("Auth:Secret").Value;

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { userName = user.Result.UserName, token = jwt });
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

    [HttpPost("bookmarkName")]
    [Authorize]
    public async Task<IActionResult> SetBookmarkName([FromBody] BookmarkNameDTO dto)
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        if (userName == null) { return Unauthorized(); }

        var userId = _userRepo.GetUserByUserName(userName).Result.UserId;

        var bookmark = await _userRepo.SetBookmarkName(userId, dto.NameId);
        if(bookmark == null) return NotFound();

        return Ok(bookmark);
    }

    [HttpPost("bookmarkTitle")]
    [Authorize]
    public async Task<IActionResult> SetBookmarkTitle([FromBody] BookmarkTitleDTO dto)
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        if (userName == null) { return Unauthorized(); }

        var userId = _userRepo.GetUserByUserName(userName).Result.UserId;

        var bookmark = await _userRepo.SetBookmarkTitle(userId, dto.TitleId);
        if(bookmark == null) return NotFound();

        return Ok(bookmark);
    }

    [HttpDelete("deletebookmarName")]
    [Authorize]
    public async Task<IActionResult> DeleteBookmarkName([FromBody] BookmarkNameDTO dto)
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        if(userName == null) { return Unauthorized(); }

        var userId = _userRepo.GetUserByUserName(userName).Result.UserId;
        if (userId == null) {return Unauthorized(); }

        var bookmark = await _userRepo.DeleteBookmarkName(userId, dto.NameId);
        if(!bookmark) { return NotFound(); }
        return Ok(bookmark);
    }

    [HttpDelete("deletebookmarkTitle")]
    [Authorize]
    public async Task<IActionResult> DeleteBookmarkTitle([FromBody] BookmarkTitleDTO dto)
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        if (userName == null) { return Unauthorized(); }

        var userId = _userRepo.GetUserByUserName(userName).Result.UserId;
        if (userId == null) { return Unauthorized(); }

        var bookmark = await _userRepo.DeleteBookMarkTitle(userId, dto.TitleId);
        if(bookmark == null) { return NotFound(); }

        return Ok(bookmark);
    }

    [HttpPost("rateName")]
    [Authorize]
    public async Task<IActionResult> RateName([FromBody] RateNameDto dto)
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        if (userName == null) { return Unauthorized(); }

        var userId = _userRepo.GetUserByUserName(userName).Result.UserId;

        var rate = await _userRepo.RateName(userId, dto.NameId, dto.Rate);
        if (rate == null) return NotFound();

        return Ok(rate);
    }

    [HttpPost("rateTitle")]
    [Authorize]
    public async Task<IActionResult> RateTitle([FromBody] RateTitleDto dto)
    {
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        if (userName == null) { return Unauthorized(); }

        var userId = _userRepo.GetUserByUserName(userName).Result.UserId;

        var rate = await _userRepo.RateTitle(userId, dto.TitleId, dto.Rate);
        if (rate == null) return NotFound();

        return Ok(rate);
    }
}

