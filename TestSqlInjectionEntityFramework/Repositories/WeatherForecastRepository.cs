using Microsoft.EntityFrameworkCore;
using TestSqlInjectionEntityFramework.Data;
using TestSqlInjectionEntityFramework.Entities;

namespace TestSqlInjectionEntityFramework.Repositories;

public class WeatherForecastRepository(DatabaseContext context)
{
    private readonly DatabaseContext _context = context;

    public async Task<IEnumerable<WeatherForecast>> GetAllBySearchAsync(string textSearch)
    {
        return await _context.WeatherForecasts
            .Where(x => x.Summary.Contains(textSearch))
            .AsNoTracking()
            .ToListAsync();
    }
}
