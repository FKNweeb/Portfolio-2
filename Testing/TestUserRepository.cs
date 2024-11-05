using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Repositories;

namespace Testing;

public class TestUserRepository
{

    [Fact]
    public async Task  GetAllUsers_NoArguments_ReturnsAllUsers()
    {
        var context = new ImdbContext(default);
        var repo = new UserRepository(context, default);

        var users = repo.GetAllAsync();

        Assert.Equal(7, users.Result.Count);
        Assert.Equal(1, users.Result.First().UserId);
    }
}
