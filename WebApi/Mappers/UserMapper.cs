using WebApi.DTO.UserDtos;
using WebApi.Models.UserRelatedModels;

namespace WebApi.Mappers;

public static class UserMapper
{
    public static UserDto ToUserDto(this User userObject)
    {
        return new UserDto
        {
            UserId = userObject.UserId,
            UserName = userObject.UserName,
            UserEmail = userObject.UserEmail
        };
    }

    public static User FromCreateUserDtoToUser(this CreateUserDto CreateUserDto)
    {
        return new User
        {
            UserId = CreateUserDto.UserId,
            UserName = CreateUserDto.UserName,
            UserPassword = CreateUserDto.UserPassword,
            UserEmail = CreateUserDto.UserEmail,
        };
    }

    public static UserInfoDTO ToUserInfoDTO(this User userObject){
        return new UserInfoDTO {
            UserName = userObject.UserName,
            UserEmail = userObject.UserEmail,
            BookMarkNames = userObject.BookMarkNames,
            BookMarkTitles = userObject.BookMarkTitles,
        };
    }
}
