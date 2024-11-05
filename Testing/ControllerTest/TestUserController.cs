using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using WebApi.Controller;
using WebApi.Data;
using WebApi.DTO.UserDtos;
using WebApi.Interfaces;
using WebApi.Mappers;
using WebApi.Models.UserRelatedModels;
using WebApi.Repositories;
using WebApi.Services;
using Xunit;

namespace Testing.ControllerTest;

public class TestUserController
{


    private const string UserApi = "http://localhost:5001/api/users";

    
    private readonly UserController _controller;

    

    [Fact]
    public async Task GetAllUsers_WithNoArguments_ReturnsOkResponse()
    {
        var (data, statusCode) = await GetArray(UserApi);

        Assert.Equal(HttpStatusCode.OK, statusCode);
    }



    //Helpers
    private async Task<(JsonArray?, HttpStatusCode)> GetArray(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonArray>(data), response.StatusCode);
    }

}

