using Microsoft.AspNetCore.Mvc;
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

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .ToListAsync();
    }
    // Is ID not a string in the database?
    public async Task<User?> GetUserById(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(e=>e.UserId == id);
    }
    public async Task<User?> GetUserByUserName(string userName)
    {
        return await _context.Users.FirstOrDefaultAsync(e=>e.UserName == userName);
    }

    public async Task<User?> CreateUser(User user)
    {
        user.UserId = await _context.Users.MaxAsync(k => k.UserId) + 1;
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> DeleteUser(int id)
    {
        var ExistingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        if(ExistingUser == null)
        {
            return null;
        }
        _context.Users.Remove(ExistingUser);
        await _context.SaveChangesAsync();
        return ExistingUser;
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

    public async Task<List<User?>> GetHistory()
    {
        return await _context.Users
            .Include(h => h.Has)
            .ThenInclude(s => s.SearchHistory)
            .ToListAsync();
    }

    public async Task<User?> GetUsersBookmarksForName(int id)
    {
        return await _context.Users
            .Include(b=>b.BookMarkNames)
            .Include(r=>r.RateNames)
                .ThenInclude(l=>l.LocalNameRating)
            .Include(b=>b.BookMarkTitles)
            .Include(r=>r.RateTitles)
                .ThenInclude(l=>l.LocalTitleRating)
            .FirstOrDefaultAsync(i=>i.UserId == id);
            
    }

  
}
