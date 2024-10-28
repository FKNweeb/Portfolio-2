using Microsoft.EntityFrameworkCore;
using WebApi.Models;
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
            .HasOne(t => t.Title)
            .WithMany(kft => kft.KnownForTitles)
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

       

       
            
    }
    public DbSet<Title> Titles { get; set; }
    
    public DbSet<TitleKnownAs> KnowAs { get; set; }

    public DbSet<TitlePlot> TitlePlots { get; set; }
    public DbSet<Name> Names { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<TitleGenre> TitleGenres { get; set; }

    public DbSet<TitleDate> TitleDates { get; set; }

    public DbSet<TitleType> TitleTypes { get; set; }   

    public DbSet<TitleIsType> TitleIsTypes{ get; set; }

    public DbSet<TitlePoster> TitlePosters { get; set; }

    public DbSet<WordIndex> WordIndexes { get; set; }
}
