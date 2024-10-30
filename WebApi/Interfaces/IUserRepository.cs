using WebApi.Models.NameRelatedModels;
using WebApi.Models.TitleRelatedModels;
using WebApi.Models.UserRelatedModels;
namespace WebApi.Interfaces;

    public interface IUserRepository
    {
       Task<List<User?>> GetAllAsync();

       Task<List<User?>> GetHistory();

       Task<List<User>> GetRateNameAndNameAsync(int page, int pageSize);

       Task<User?> GetUsersBookmarksForName(int id);
       int NumberOfUsers();
    }

