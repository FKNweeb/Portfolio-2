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
}
