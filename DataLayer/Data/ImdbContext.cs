using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data;

public class ImdbContext : DbContext
{

   
    public ImdbContext(DbContextOptions options) : base(options)
    { }
    public DbSet<Title> Titles { get; set; }
}
