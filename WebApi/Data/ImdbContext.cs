using Microsoft.EntityFrameworkCore;
using WebApi.Models;

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
            
    }
    public DbSet<Title> Titles { get; set; }
    public DbSet<TitleKnowAs> KnowAs { get; set; }

    public DbSet<TitlePlot> TitlePlots { get; set; }
    public DbSet<Name> Names { get; set; }
}
