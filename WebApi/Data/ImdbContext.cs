using System.IO.Compression;
using Microsoft.EntityFrameworkCore;
using WebApi.Models.NameRelatedModels;
using WebApi.Models.TitleRelatedModels;

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
        modelBuilder.Entity<Language>()
            .HasOne(l => l.TitleKnownAs)
            .WithMany(l => l.Languages)
            .HasForeignKey(l => new {l.TitleId,  l.OrderingAkas});

        // Map ProfessionName and Professions
        modelBuilder.Entity<ProfessionName>()
            .HasOne(p => p.Name)
            .WithMany(n => n.ProfessionNames)
            .HasForeignKey(p => p.NameId);

        modelBuilder.Entity<ProfessionName>()
            .HasOne(p => p.Profession)
            .WithMany(p => p.ProfessionNames)
            .HasForeignKey(p => p.ProfessionTitle);

    }
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
}
