using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.NameRelatedModels;
using WebApi.Models.TitleRelatedModels;
using WebApi.Models.UserRelatedModels;


namespace WebApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ImdbContext _context;

    public UserRepository(ImdbContext context)
    {
        _context = context;
    }

    public async Task<List<User?>> GetAllAsync()
    {
        return await _context.Users
            .ToListAsync();
    }

    public async Task<List<User>> GetRateNameAndNameAsync(int page, int pageSize){
        return await _context.Users
                .Include(u => u.RateNames)
                .ThenInclude(rn => rn.Name)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
    }

    public int NumberOfUsers()
    {
        return _context.Users.Count();
    }
}
