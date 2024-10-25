using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class ImdbContext : DbContext
{
    public ImdbContext(DbContextOptions options) : base(options)
    { }
    public DbSet<Title> Titles { get; set; }
    public DbSet<TitleKnowAs> KnowAs { get; set; }
}
