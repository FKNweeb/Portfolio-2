using Microsoft.EntityFrameworkCore;

using WebApi.Data;

using WebApi.Models.UserRelatedModels;
using WebApi.Repositories;

namespace Testing;

public class TestUserRepository
{
    private readonly UserRepository _userRepository;
   
    private readonly ImdbContext _context;

    public TestUserRepository()
    {
        var options = new DbContextOptionsBuilder<ImdbContext>()
            .UseInMemoryDatabase(databaseName:"TestDatabase")
            .Options;

        _context = new ImdbContext(options);
        _userRepository = new UserRepository(_context, default);

        seedDatabase();
    }

    private void seedDatabase()
    {
        var users = new List<User>
        { new User
            {
                UserId = 1,
                UserName = "Test",
                UserEmail = "Test",
                UserPassword = "Test",
                Salt = "",
             },
             new User
             {
                UserId = 2,
                UserName = "Test2",
                UserEmail = "Test2",
                UserPassword = "Test2",
                Salt = "",
             },

        };

        _context.Users.AddRange(users);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetAllUsers_NoArguments_ReturnsAllUsers()
    {
        var users = await _userRepository.GetAllAsync();

        Assert.NotNull(users);
        Assert.Equal(2, users.Count);
        Assert.Equal(1, users.First().UserId);
    }

    [Fact]
    public async Task GetUserById_WithArguments_ReturnsUser()
    {
        var id = 1;
        var user = await _userRepository.GetUserById(1);

        Assert.NotNull(user);
        Assert.Equal(1, user.UserId);
        Assert.Equal("Test", user.UserName);
        Assert.Equal("Test", user.UserEmail);
        Assert.Equal("Test", user.UserPassword);
    }

    [Fact]
    public async Task GetUserById_WithArguments_ReturnsNull()
    {
        var id = 7;
        var user = _userRepository.GetUserById(id);

        Assert.Null(user.Result);
    }

    [Fact]
    public async Task CreateUser_WithArguments_ReturnsUsers()
    {
        var newUser = new User
        {
            UserName = "Test3",
            UserEmail = "Test3",
            UserPassword = "Test3",   
        };

        var user = await _userRepository.CreateUser(newUser);

        Assert.NotNull(user);
        Assert.Equal(3, user.UserId);
        Assert.Equal("Test3", user.UserName);
        Assert.Equal("Test3", user.UserEmail);
        Assert.Equal("Test3", user.UserPassword);

        await _userRepository.DeleteUser(user.UserId);
        var users = await _userRepository.GetAllAsync();
        Assert.Equal(2, users.Count);
    }

    [Fact]
    public async Task CreateUser_WithArguments_ReturnsUserExists()
    {
        
        var user = new User
        {
            UserName = "Test2",
            UserEmail = "Test2",
            UserPassword = "Test2",

        };

        var result = await _userRepository.CreateUser(user);
        var users = await _userRepository.GetAllAsync();
        //Assert.IsType<User>(result);
        Assert.Null(result);
        Assert.Equal(2, users.Count);
    }

    [Fact]
    public async Task DeleteUser_WithArgumnets_ReturnsDeletedUser()
    {
        var id = 2;

        var deletedUser = await _userRepository.DeleteUser(id);
        var users = await _userRepository.GetAllAsync();

        Assert.NotNull(deletedUser);
        Assert.Equal(1, users.Count);
    }
}
