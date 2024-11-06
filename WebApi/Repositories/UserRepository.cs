using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models.FunctionBasedModels;
using WebApi.Models.NameRelatedModels;
using WebApi.Models.TitleRelatedModels;
using WebApi.Models.UserRelatedModels;
using static Azure.Core.HttpHeader;


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
        try { user.UserId = await _context.Users.MaxAsync(k => k.UserId) + 1; }
        catch (Exception) { user.UserId = 1; }
        User? existingUser = await _context.Users.FirstOrDefaultAsync(e=>e.UserEmail == user.UserEmail);
        if(user.UserEmail == existingUser?.UserEmail ) { return null; }
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
        var lastrecord = 0;
        try
        {
            lastrecord = await _context.SearchHistories.MaxAsync(u => u.HistoryId);
        }
        catch (Exception ex) { }

        var SearchEntry = new SearchHistory
        {
            HistoryId = lastrecord + 1,
            Description = keyword
        };

        await _context.SearchHistories.AddAsync(SearchEntry);
        await _context.SaveChangesAsync();

        return await _context.SearchHistories.MaxAsync(u => u.HistoryId) > lastrecord;
    }

    public async Task<SetBookmarkName> SetBookmarkName(int userId, string nameId)
    {
        var result = await _context.Set<SetBookmarkName>()
                             .FromSqlInterpolated($"SELECT set_bookmark_name({userId}, {nameId}) AS \"IsBookmarkName\"")
                             .FirstOrDefaultAsync();
        return result;
    
    }

    public async Task<SetBookmarkTitle> SetBookmarkTitle(int userId, string titleId)
    {
        var result = await _context.Set<SetBookmarkTitle>()
                             .FromSqlInterpolated($"SELECT set_bookmark_title({userId}, {titleId}) AS \"IsBookmarkTitle\"")
                             .FirstOrDefaultAsync();    
        return result;
    
    }

    
    public async Task<bool> DeleteBookmarkName(int userId, string nameId)
    {
        var bookmark = await _context.bookMarkNames.FirstOrDefaultAsync(n=>n.UserId == userId && n.NameId == nameId);

        if (bookmark == null)
        {
            return false;
        }

        _context.bookMarkNames.Remove(bookmark);
        await _context.SaveChangesAsync();
        return true;
    }
   

    

    public async Task<bool> DeleteBookMarkTitle(int userId, string titleId)
    {
       var bookmark = await _context.bookMarkTitles.FirstOrDefaultAsync(n=>n.UserId == userId && n.TitleId == titleId);
        if(bookmark == null)
        {
            return false;
        }
        _context.bookMarkTitles.Remove(bookmark);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<SetRateName> RateName(int userId, string nameId, int vote)
    {
        var result = await _context.Set<SetRateName>()
                              .FromSqlInterpolated($"SELECT set_rate_name({userId},{nameId},{vote}) AS \"IsSetRateName\"")
                              .FirstOrDefaultAsync();
        return result;
    }

    public async Task<SetRateTitle> RateTitle(int userId, string titleId, int vote)
    {
        var result = await _context.Set<SetRateTitle>()
                              .FromSqlInterpolated($"SELECT set_rate_title({userId},{titleId},{vote}) AS \"IsSetRateTitle\"")
                              .FirstOrDefaultAsync();
        return result;
    }

   

}
