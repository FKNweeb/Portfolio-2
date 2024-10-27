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

       
            
    }
    public DbSet<Title> Titles { get; set; }
    public DbSet<TitleKnowAs> KnowAs { get; set; }

    public DbSet<TitlePlot> TitlePlots { get; set; }
    public DbSet<Name> Names { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<TitleGenre> TitleGenres { get; set; }

    public DbSet<TitleDate> TitleDates { get; set; }
}
