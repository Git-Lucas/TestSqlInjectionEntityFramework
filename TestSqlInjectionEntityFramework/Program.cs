using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestSqlInjectionEntityFramework.Data;
using TestSqlInjectionEntityFramework.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionStringMySql = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string was not found");
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(connectionStringMySql, ServerVersion.AutoDetect(connectionStringMySql)));

builder.Services.AddScoped<WeatherForecastRepository>();

var app = builder.Build();

IServiceScope scope = app.Services.CreateScope();
DatabaseContext databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
await databaseContext.Database.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/testSqlInjectionInEntityFramework", ([FromServices] WeatherForecastRepository weatherForecastRepository, [FromQuery] string sqlInjectionInSearchText = "' OR 1=1;") =>
{
    return weatherForecastRepository.GetAllBySearchAsync(sqlInjectionInSearchText);
})
.WithName("GetWeatherForecast")
.WithOpenApi();

await app.RunAsync();
