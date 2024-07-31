using Microsoft.EntityFrameworkCore;
using TestSqlInjectionEntityFramework.Entities;

namespace TestSqlInjectionEntityFramework.Data;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
}
