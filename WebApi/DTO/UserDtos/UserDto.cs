using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WebApi.DTO.UserDtos;

public class UserDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }

    public string UserEmail { get; set; }

    public override string? ToString()
    {
        return ($"{UserId}, {UserName}, {UserEmail}"); 
    }
}
