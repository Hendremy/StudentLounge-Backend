using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentLounge_Backend.Models;
using StudentLounge_Backend.Models.Seed;
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
    });

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddScoped<ICreateToken, JwtTokenCreator>(creator => 
    new JwtTokenCreator(config["JWT:Key"],config["JWT:Issuer"],config["JWT:Audience"]));

var app = builder.Build();

//Seeding standard roles and admin account
using (var scope = app.Services.CreateScope())
{
    var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<StudentLoungeUser>>();
    var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    UserSeed.AddDefaultRoles(rolemanager);
    UserSeed.AddDefaultUser(usermanager);
}

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
