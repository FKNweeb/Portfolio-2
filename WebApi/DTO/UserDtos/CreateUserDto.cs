namespace WebApi.DTO.UserDtos;

public class CreateUserDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }

    public string UserPassword { get; set; }
    public string UserEmail { get; set; }
}
