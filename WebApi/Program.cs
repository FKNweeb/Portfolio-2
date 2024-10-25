using DataLayer.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMapster();

builder.Services.AddControllers();

builder.Services.AddDbContext<ImdbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvcCore();

builder.Services.AddScoped<ITitlteRepository, TitleRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();
app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
