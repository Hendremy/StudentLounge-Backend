using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentLounge_Backend.Models.Authentication;
using StudentLounge_Backend.Models.Authentication.Seed;
using StudentLounge_Backend.Models;
using System.Text;
using System.Text.Json.Serialization;
using StudentLounge_Backend.Models.UploadFile;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<FileUploadFilter>();
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("default"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["JWT:Issuer"],
            ValidAudience = config["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]))
        };
    });

builder.Services.AddScoped<ICreateToken, JwtTokenCreator>(creator => 
    new JwtTokenCreator(config["JWT:Key"],config["JWT:Issuer"],config["JWT:Audience"]));

builder.Services.AddScoped<IHandleUsers, UserRepository>();
builder.Services.AddScoped<IHandleAuth, AuthenticationHandler>(services =>
{
    var userHandler = services.GetRequiredService<IHandleUsers>();
    return new AuthenticationHandler(userHandler);
});
builder.Services.AddScoped<IHandleExternalAuth, ExternalAuthHandlers>(services =>
{
    var userHandler = services.GetRequiredService<IHandleUsers>();
    return new ExternalAuthHandlers(userHandler);
});
var app = builder.Build();

//Seeding standard roles and admin account
using (var scope = app.Services.CreateScope())
{
    var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
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
