﻿using WebApi.Models.FunctionBasedModels;
using WebApi.Models.NameRelatedModels;
using WebApi.Models.TitleRelatedModels;
using WebApi.Models.UserRelatedModels;
namespace WebApi.Interfaces;
public interface IUserRepository
{
    Task<User?> GetUserById(int id);
    Task<User?> GetUserByUserName(string userName);
    Task<List<User>> GetAllAsync();

    Task<User?> CreateUser(User user);

    Task<User?> DeleteUser(int id);

    Task<List<SearchHistory>> GetHistory();

    Task<bool> UpdateSearchHistory(string keyword);

    Task<SetBookmarkName> SetBookmarkName(int userId, string nameId);
    Task<SetBookmarkTitle> SetBookmarkTitle(int userId, string titleId);
    
    Task<bool> DeleteBookmarkName(int userId, string nameId);

    Task<bool> DeleteBookMarkTitle(int userId, string titleId);

    Task<SetRateName> RateName(int userId, string nameId, int vote);
    Task<SetRateTitle> RateTitle(int userId, string titleId, int vote);

    int NumberOfUsers();
}

