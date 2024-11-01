using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebApi.Models.FunctionBasedModels;
using WebApi.Models.NameRelatedModels;
using WebApi.Models.TitleRelatedModels;
using WebApi.Models.UserRelatedModels;

namespace WebApi.Data;

public class ImdbContext : DbContext
{
    public ImdbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Title>()
            .HasOne(t => t.TitlePlot)
            .WithOne(tp => tp.Title)
            .HasForeignKey<TitlePlot>(tp => tp.TitlePlotId);

        modelBuilder.Entity<TitleGenre>()
            .HasOne(tg => tg.Title)
            .WithMany(t=> t.TitleGenres)
            .HasForeignKey(tg => tg.TitleId);

        modelBuilder.Entity<TitleGenre>()
            .HasOne(tg => tg.Genre)
            .WithMany(g => g.TitleGenres)
            .HasForeignKey(tg => tg.GenreName);

        modelBuilder.Entity<Title>()
            .HasOne(t => t.TitleDate)
            .WithOne(td => td.Title)
            .HasForeignKey<TitleDate>(td=> td.TitleId);

        //Name and KnowForTitles
        modelBuilder.Entity<Name>()
            .HasMany(e => e.KnownForTitles)
            .WithOne(e => e.Name)
            .HasForeignKey(e => e.NameId);

        modelBuilder.Entity<Title>()
            .HasMany(e=>e.KnownForTitles)
            .WithOne(e=>e.Title)
            .HasForeignKey(e => e.TitleId);


        modelBuilder.Entity<KnownForTitle>()
            .HasOne(kft => kft.Title)
            .WithMany(t => t.KnownForTitles)
            .HasForeignKey(t => t.TitleId);

        modelBuilder.Entity<KnownForTitle>()
            .HasOne(kft => kft.Name)
            .WithMany(n => n.KnownForTitles)
            .HasForeignKey(kft => kft.NameId);

        //Map Title to TitleIsTypes
        modelBuilder.Entity<TitleIsType>()
            .HasOne(t=>t.Title)
            .WithOne(t=>t.TitleIsType)
            .HasForeignKey<TitleIsType>(td=> td.TitleId);

        //Map TitlIsType to TitleType
        modelBuilder.Entity<TitleIsType>()
            .HasOne(t => t.TitleType)
            .WithOne(t => t.TitleIsType)
            .HasForeignKey<TitleIsType>(td => td.TypeOfTitle);

        //Map Title and TitlePoster
        modelBuilder.Entity<Title>()
            .HasOne(tp => tp.TitlePoster)
            .WithOne(t => t.Title)
            .HasForeignKey<TitlePoster>(t => t.TitleId);


        //Map Title and WordIndex
        modelBuilder.Entity<WordIndex>()
             .HasOne(t => t.Title)
             .WithMany(t => t.WordIndexes)
             .HasForeignKey(t => t.TitleId);

        modelBuilder.Entity<Title>()
            .HasMany(t => t.WordIndexes)
            .WithOne(wi => wi.Title)
            .HasForeignKey(wi => wi.TitleId);


        //Map TitleKnowAs And Language
        modelBuilder.Entity<TitleKnownAs>()
             .HasOne(t => t.Language)
             .WithOne(t => t.TitleKnownAs)
             .HasForeignKey<Language>(l => new { l.TitleId, l.OrderingAkas });

        modelBuilder.Entity<Title>()
            .HasMany(e => e.TitleKnownAs)
            .WithOne(e => e.Title)
            .HasForeignKey(e => e.TitleId);


        //Map TitleKnownAs and Region
        modelBuilder.Entity<TitleKnownAs>()
            .HasOne(r => r.Region)
            .WithOne(t => t.TitleKnownAs)
            .HasForeignKey<Region>(e => new { e.RegionId, e.RegionOrdering });



        // Map ProfessionName and Professions
        modelBuilder.Entity<ProfessionName>()
            .HasOne(p => p.Name)
            .WithMany(n => n.ProfessionNames)
            .HasForeignKey(p => p.NameId);

        modelBuilder.Entity<ProfessionName>()
            .HasOne(p => p.Profession)
            .WithMany(p => p.ProfessionNames)
            .HasForeignKey(p => p.ProfessionTitle);

        //Map Episode
        modelBuilder.Entity<Episode>()
            .HasOne(t=>t.Title)
            .WithMany(t=>t.Episodes)
            .HasForeignKey(f=>f.TitleId);

        //Map Title to BookmarkTitle
        modelBuilder.Entity<Title>()
            .HasMany(b => b.bookMarkTitles)
            .WithOne(b => b.Title)
            .HasForeignKey(t => t.TitleId);

        //Map user to Has and Search History
        modelBuilder.Entity<Has>()
            .HasOne(tg => tg.User)
            .WithMany(t => t.Has)
            .HasForeignKey(tg => tg.UserId);

        modelBuilder.Entity<Has>()
            .HasOne(tg => tg.SearchHistory)
            .WithMany(g => g.Has)
            .HasForeignKey(tg => tg.HistoryId);

        modelBuilder.HasDbFunction(() => StringSearch(default))
            .HasName("string_search");
        modelBuilder.HasDbFunction(() => StructuredStringSearch(default, default, default, default))
            .HasName("structured_string_search");
        modelBuilder.HasDbFunction(() => BestMatch(default))
            .HasName("best_match");
    }

    public IQueryable<SearchResult> StringSearch (string keyword)
        => FromExpression(()=> StringSearch(keyword));

    public IQueryable<SearchResult> StructuredStringSearch(string k1, string k2, string k3, string k4)
        => FromExpression(() => StructuredStringSearch(k1, k2, k3, k4));

    public IQueryable<BestMatch> BestMatch(string [] keywords)
        =>FromExpression(()=> BestMatch(keywords));


    public DbSet<Title> Titles { get; set; }

    public DbSet<TitleKnownAs> KnowAs { get; set; }

    public DbSet<TitlePlot> TitlePlots { get; set; }
    public DbSet<Name> Names { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<TitleGenre> TitleGenres { get; set; }

    public DbSet<TitleDate> TitleDates { get; set; }
    public DbSet<KnownForTitle> KnownForTitles { get; set; }

    public DbSet<TitleType> TitleTypes { get; set; }

    public DbSet<TitleIsType> TitleIsTypes{ get; set; }

    public DbSet<TitlePoster> TitlePosters { get; set; }

    public DbSet<WordIndex> WordIndexes { get; set; }

    public DbSet<Language> Languages { get; set; }
    public DbSet<ProfessionName> ProfessionNames { get; set; }
    public DbSet<Profession> Professions { get; set; }
    public DbSet<CrewCharacter> CrewCharacters { get; set; }

    public DbSet<Region> Regions { get; set; }

    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Crew> Crews { get; set; }
    public DbSet<CrewJob> CrewJobs { get; set; }
    public DbSet<Job> Jobs { get; set; }

    public DbSet<SearchHistory> SearchHistories { get; set; }

    public DbSet<Has> Has { get; set; }

    public DbSet<BookMarkTitle> bookMarkTitles { get; set; }

    public DbSet<BookMarkName> bookMarkNames { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RateTitle> RateTitles { get; set; }
    public DbSet<LocalTitleRating> LocalTitleRatings { get; set; }

    public DbSet<LocalNameRating> LocalNameRatings { get; set; } 
    public DbSet<RateName> RateNames { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DbSet<SearchResult> SearchResults { get; set; }

    public DbSet<BestMatch> BestMatches { get; set; }
}
