using activity.infrastructure;
using activity.infrastructure.Middleware;
using activity.infrastructure.Seeding;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation.AspNetCore;
using activity.api.Mapper;
using FluentValidation;
using activity.api.CQRS_Functions.Command.ActivityCommand;
using activity.api.DTO.ActivityDto.Validators;
using activity.api.DTO.ActivityDto;
using activity.api.Extensions;
using Microsoft.AspNetCore.Identity;
using activity.domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.GetInfrastuctureServices();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "https://localhost:3000");
    });
});

builder.Services.GetValidators();
builder.Services.ExtendServicesByNugets();
builder.Services.AddIdentityServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();//use in production
app.UseCors("CorsPolicy");
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();

app.MapControllers();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context,userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Error during migration");
}

app.Run();
