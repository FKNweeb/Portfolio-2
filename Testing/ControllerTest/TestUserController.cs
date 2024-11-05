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
        Assert.Equal(7, data?.Count);
        Assert.Equal("1", data?.FirstElement("userId"));
        Assert.Equal("7", data?.LastElement("userId"));

    }

    [Fact]
    public async Task GetUserById_WithArgumen_ValidId_ReturnsOkResponse()
    {
        var (user, statusCode) = await GetObject($"{UserApi}/1");

        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal("1", user?.Value("userId"));
        Assert.Equal("Jia Zhiyuan", user?.Value("userName"));
        Assert.Equal("jiazhiyuan@gmail.com", user?.Value("userEmail"));

    }


    [Fact]
    public async Task GetUserById_WithArgument_NotValidId_ReturnsNotFoundResponse()
    {
        var (_, statusCode) = await GetObject($"{UserApi}/101");
        Assert.Equal(HttpStatusCode.NotFound, statusCode);
    }



    //Helpers
    private async Task<(JsonArray?, HttpStatusCode)> GetArray(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonArray>(data), response.StatusCode);
    }

    private async Task<(JsonObject?, HttpStatusCode)> GetObject(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
    }



}
static class HelperExt
    {
        public static string? Value(this JsonNode node, string name)
        {
            var value = node[name];
            return value?.ToString();
        }

        public static string? FirstElement(this JsonArray node, string name)
        {
            return node.First()?.Value(name);
        }

        public static string? LastElement(this JsonArray node, string name)
        {
            return node.Last()?.Value(name);
        }
    }

