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
    private readonly INameRepository _nameRepository;

    public UserRepository(ImdbContext context, INameRepository nameRepository)
    {
        _context = context;
        _nameRepository = nameRepository;
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

    public async Task<List<SearchHistory>> GetHistory()
    {
        return await _context.SearchHistories
            .ToListAsync();
    }
    public async Task<bool> UpdateSearchHistory(string keyword)
    { 
        var lastrecord = await _context.SearchHistories.MaxAsync(u => u.HistoryId);

        var SearchEntry = new SearchHistory
        {
            HistoryId = (int)lastrecord + 1,
            Description = keyword
        };

        await _context.SearchHistories.AddAsync(SearchEntry);
        await _context.SaveChangesAsync();

        return await _context.SearchHistories.MaxAsync(u => u.HistoryId) > lastrecord;
    }

    public async Task<BookMarkName?> SetBookmarkName(int userId, string name)
    {
        throw new NotImplementedException();    
    }

    public Task<bool> DeleteBookmarkName()
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetBookarkTitle()
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBookMarkTitle()
    {
        throw new NotImplementedException();
    }

    public Task<bool> RateName()
    {
        throw new NotImplementedException();
    }

    public Task<bool> RateTitle()
    {
        throw new NotImplementedException();
    }

    //public async Task<User?> GetUsersBookmarksForName(int id)
    //{
    //    return await _context.Users
    //        .Include(b=>b.BookMarkNames)
    //        .Include(r=>r.RateNames)
    //            .ThenInclude(l=>l.LocalNameRating)
    //        .Include(b=>b.BookMarkTitles)
    //        .Include(r=>r.RateTitles)
    //            .ThenInclude(l=>l.LocalTitleRating)
    //        .FirstOrDefaultAsync(i=>i.UserId == id);

    //}

}
