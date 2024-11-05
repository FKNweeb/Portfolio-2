using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using WebApi.Controllers;
using Xunit;

namespace Testing.ControllerTest;

public class TestTitleController
{
    private const string TitleApi = "http://localhost:5001/api/titles";

    private readonly TitleController _controller;

    [Fact]
    public async Task GetAllUsers_WithNoArguments_ReturnsOkResponse()
    {
        var (jsonObject, statusCode) = await GetArrayObject(TitleApi);

        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal(130036, jsonObject?["numberOfItems"]?.GetValue<int>());
        Assert.Equal("Train to Busan - Movie REACTION!!", jsonObject?["items"]?.AsArray()?.First()?["primaryTitle"]?.GetValue<string>());
    }

    [Fact]
    public async Task SimilarTitles_WithId_ReturnsOkResponse()
    {
        var (titles, statusCode) = await GetArray($"{TitleApi}/similartitles/tt15235848");

        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal("tt0052520", titles.FirstElement("tconst"));
        Assert.Equal("The Twilight Zone", titles.FirstElement("primary_title"));
    }

    [Fact]
    public async Task SimilarTitles_WithoutId_ReturnsNotFoundResponse()
    {
        var (titles, statusCode) = await GetArrayObject($"{TitleApi}/similartitles/1234");

        Assert.Equal(HttpStatusCode.NotFound, statusCode);
    }

    [Fact]
    public async Task GetAllTitleByGenre_WithNoArugments_ReturnsOkResponse()
    {
        var (jsonObject, statusCode) = await GetArrayObject($"{TitleApi}/genre");

        Assert.Equal(HttpStatusCode.OK, statusCode);
        var firstItem = jsonObject?["items"]?.AsArray()?.First() as JsonObject;
        
        Assert.Equal("Action", firstItem?["genreName"]?.GetValue<string>());
        
        //Only god understands this
        var titleNode = firstItem?["titles"];

        var titleArray = titleNode as JsonArray;

        var firstTitle = titleArray?.FirstOrDefault() as JsonObject;

        var titleValue = firstTitle?["title"].GetValue<string>();

        Assert.Equal("Vazir", titleValue);
    }

    //Helpers
    private async Task<(JsonArray?, HttpStatusCode)> GetArray(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonArray>(data), response.StatusCode);
    }

    private async Task<(JsonObject?, HttpStatusCode)> GetArrayObject(string url)
    {
        var client = new HttpClient();
        var response = await client.GetAsync(url);
        var data = await response.Content.ReadAsStringAsync();

        // Deserialize as JsonObject since the response is an object with paging info and data array
        var jsonObject = JsonSerializer.Deserialize<JsonObject>(data);
        return (jsonObject, response.StatusCode);
    }
}

