using WebApi.Models.NameRelatedModels;
using WebApi.Models.TitleRelatedModels;
using WebApi.Models.UserRelatedModels;
namespace WebApi.Interfaces;
public interface IUserRepository
{
    Task<User?> GetUserById(int id);
    Task<List<User>> GetAllAsync();

    Task<User?> CreateUser(User user);

    Task<User?> DeleteUser(int id);

    Task<List<User?>> GetHistory();

    Task<List<User>> GetRateNameAndNameAsync(int page, int pageSize);

    Task<User?> GetUsersBookmarksForName(int id);
    int NumberOfUsers();
}

