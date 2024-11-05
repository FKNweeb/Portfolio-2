using Microsoft.EntityFrameworkCore;

using WebApi.Data;
using WebApi.Models.TitleRelatedModels;
using WebApi.Models.UserRelatedModels;
using WebApi.Repositories;

namespace Testing;

public class TestTitleRepository
{
    private readonly TitleRepository _titleRepository;
   
    private readonly ImdbContext _context;

    public TestTitleRepository()
    {
        var options = new DbContextOptionsBuilder<ImdbContext>()
            .UseInMemoryDatabase(databaseName:"TestDatabase")
            .Options;

        _context = new ImdbContext(options);
        _titleRepository = new TitleRepository(_context);

        seedDatabase();
    }

    private void seedDatabase()
    {
        var titles = new List<Title> {
            new Title {
                TitleId = "tt7900416",
                PrimaryTitle = "In The Beginning",
                TitlePlot = new TitlePlot { Plot = "Alex wonders how he got to where he is which has him thinking about when he first moved in with Quincy and Tamika and what led up to their tryst." } ,
                TitleDate = new TitleDate{ StartYear = "2018", EndYear = "    " },
                TitleGenres = null,
                TitleKnownAs = null,
                TitlePoster = null,
                TitleIsType = new TitleIsType { TypeOfTitle = "tvEpisode" }
            }
        };

        _context.Titles.AddRange(titles);
        _context.SaveChanges(); 
    }

    [Fact]
    public async Task GetAllTitles_NoArguments_ReturnsAllTitles()
    {
        var titles = _titleRepository.GetAllAsync(0, 25);

        Assert.NotNull(titles);
        Assert.Single(titles.Result);
        Assert.Equal("tt7900416", titles.Result.First().TitleId);
    }

    [Fact]
    public async Task GetNumberOfTitles_NoArguments_ReturnsNumberOfTitles()
    {
        var titlesCount = _titleRepository.NumberOfTitles();

        Assert.NotNull(titlesCount);
        Assert.Equal(1, titlesCount);
    }

    [Fact]
    public async Task GetTitleById_WithArgument_ReturnsTitle()
    {
        var title = _titleRepository.GetTitleById("tt7900416");

        Assert.NotNull(title);
        Assert.Equal("tt7900416", title.Result.TitleId);
        Assert.Equal("In The Beginning", title.Result.PrimaryTitle);
    }

    [Fact]
    public async Task GetTitleById_WithInvalidArgument_ReturnsTitle()
    {
        var title = _titleRepository.GetTitleById("");

        Assert.Null(title.Result);
    }
}
