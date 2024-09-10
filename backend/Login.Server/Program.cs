using Login.Server;
using Login.Server.Context;
using Login.Server.Mapping;
using Login.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(configuration =>
{
    configuration.AddDefaultPolicy(options =>
    options
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .AllowAnyHeader());
});

builder.Services.AddControllers();




builder.Services.AddDbContext<ApplicationDbContext>(p =>
{
    p.UseInMemoryDatabase("MyDb");
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<JwtProvider>();

builder.Services
    .AddIdentity<AppUser, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
