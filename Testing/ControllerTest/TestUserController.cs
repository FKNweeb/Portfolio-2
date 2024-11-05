using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controller;
using WebApi.DTO.UserDtos;
using WebApi.Interfaces;
using WebApi.Mappers;
using WebApi.Models.UserRelatedModels;
using WebApi.Services;
using Xunit;

namespace Testing.ControllerTest;

public class TestUserController
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly UserController _controller;
    private readonly Mock<Hashing> _hashing;

    public TestUserController()
    {
        _mockRepo = new Mock<IUserRepository>();
        _hashing = new Mock<Hashing>();
        _controller = new UserController(_mockRepo.Object, default, default, _hashing.Object);
    }
    [Fact]
    public async Task GetAllUsers_WithNoArguments_ReturnOkAndAllUsers()
    {
        // Given
        var users = new List<User> { new User
        {
            UserId = 101,
            UserName = "Xunit",
            UserPassword = "1234",
            UserEmail = "xunit",
            Salt = "" }
        };

        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);
        // When
        var result = await _controller.GetAllUsers();
        // Then
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<UserDto>>(okResult.Value);
        Assert.Single(returnValue);
    }

    [Fact]
    public async Task GetAllUsersWith_NoArguments_ReturnNotFound()
    {
        //Given
        _mockRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync((List<User>)null);
        //When
        var result = await _controller.GetAllUsers();
        //Then
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task GetUserById_WithArguments_ReturnsOKAndUser()
    {
        var userId = 101;
        var user = new User
        {
            UserId = 101,
            UserName = "Xunit",
            UserPassword = "1234",
            UserEmail = "xunit",
            Salt = ""
        };
        _mockRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(user);

        var result = await _controller.GetUserById(userId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<UserDto>(okResult.Value);
        Assert.Equal(userId, returnValue.UserId);
    }

    [Fact]
    public async Task GetUserById_WithArguments_ReturnsNotFound()
    {
        var userId = 100;

        _mockRepo.Setup(repo => repo.GetUserById(userId)).ReturnsAsync((User)null);

        var result = await _controller.GetUserById(userId);

        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);

        Assert.Equal("User does not exist", notFoundResult.Value);

    }

    [Fact]
    public async Task CreateUser_WithArguments_ReturnsCreatesUser()
    {
        // Arrange
        var createUserDto = new CreateUserDto
        {
            UserName = "newUser",
            UserPassword = "password123"
        };

        var hashedPwd = "hashedPassword";
        var salt = "saltValue";
        var user = new User { UserId = 1, UserName = createUserDto.UserName };

        _mockRepo.Setup(repo => repo.GetUserByUserName(createUserDto.UserName))
                  .ReturnsAsync((User)null); // No existing user
        _hashing.Setup(h => h.Hash(createUserDto.UserPassword))
                    .Returns((hashedPwd, salt)); // Return hashed password and salt
        _mockRepo.Setup(repo => repo.CreateUser(It.IsAny<User>()))
                  .ReturnsAsync(user); // Simulate user creation

        // Act
        var result = await _controller.CreateUser(createUserDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal("GetUserById", createdResult.ActionName);
        Assert.Equal(1, ((UserDto)createdResult.Value).UserId);
    }
}

