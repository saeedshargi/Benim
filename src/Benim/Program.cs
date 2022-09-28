using Benim.Domain.Entities;
using Benim.Domain.Interfaces;
using Benim.Extensions;
using Benim.Infrastructure.Data;
using Benim.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = Path.Join(path, "benim.db");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BenimContext>(options =>
{
    options.UseSqlite($"Data Source = {dbPath}");
});

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddIdentity<User<int>, Role<int>>(options => { options.SignIn.RequireConfirmedAccount = true; })
    .AddEntityFrameworkStores<BenimContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthenticationWithJwt(builder.Configuration);


builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

SeedData.AddDefaultData(app);

app.Run();
