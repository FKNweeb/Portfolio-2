using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Repositories;
using Microsoft.IdentityModel.Tokens;
using WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ImdbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton(new Hashing());
builder.Services.AddScoped<ITitlteRepository, TitleRepository>();
builder.Services.AddScoped<INameRepository, NameRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();

var secret = builder.Configuration.GetSection("Auth:Secret").Value;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ClockSkew = TimeSpan.Zero
        }
    );

//CORS policy to enable requests from our frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("allowed", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();
// app.UseHttpsRedirection();

app.UseCors("allowed");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
