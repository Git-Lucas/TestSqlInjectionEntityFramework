using Microsoft.EntityFrameworkCore;
using TestSqlInjectionEntityFramework.Data;
using TestSqlInjectionEntityFramework.Entities;

namespace TestSqlInjectionEntityFramework.Repositories;

public class WeatherForecastRepository(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

    public async Task<IEnumerable<WeatherForecast>> GetAllBySearchAsync(string textSearch)
    {
        IQueryable<WeatherForecast> query = _context.WeatherForecasts
            .FromSqlRaw("SELECT * FROM WeatherForecasts WHERE Summary LIKE '%' + {0} + '%'", textSearch)
            //.FromSqlRaw($"SELECT * FROM WeatherForecasts WHERE Summary = '{textSearch}';")
            .AsNoTracking();

        return await query.ToListAsync();
    }
}
