using System.Text;
using TravelApi.Models;
using TravelApi.Filter;
using TravelApi.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TravelApiContext>(
    dbContextOptions => dbContextOptions.UseMySql(
        builder.Configuration["ConnectionStrings:DefaultConnection"],
        ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"])
    ) 
);


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<TravelApiContext>()
    .AddDefaultTokenProviders();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthentication(); // Add authentication middleware before authorization middleware
app.UseAuthorization();

app.MapControllers();

app.Run();