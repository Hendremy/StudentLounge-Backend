using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentLounge_Backend.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StudentLoungeDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("default"));
});

builder.Services.AddIdentity<StudentLoungeUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<StudentLoungeDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
        };
    })
    .AddGoogle(options => {
        var googleCreds = config.GetSection("Authentication:Google");
        options.ClientId = googleCreds["ClientId"];
        options.ClientSecret = googleCreds["ClientSecret"];
    })
    .AddFacebook(options => {
        var facebookCreds = config.GetSection("Authentication:Facebook");
        options.AppId = facebookCreds["AppId"];
        options.AppSecret = facebookCreds["AppSecret"];
    });

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddScoped<ICreateToken, JwtTokenCreator>(creator => 
    new JwtTokenCreator(config["JWT:Key"],config["JWT:Issuer"],config["JWT:Audience"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
