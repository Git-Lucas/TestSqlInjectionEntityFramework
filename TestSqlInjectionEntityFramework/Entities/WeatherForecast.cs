namespace TestSqlInjectionEntityFramework.Entities;

public class WeatherForecast
{
    public Guid Id { get; private set; }
    public DateOnly Date { get; private set; }
    public int TemperatureC { get; private set; }
    public string Summary { get; private set; }
    public int TemperatureF { get; private set; }

    public WeatherForecast(DateOnly date, int temperatureC, string? summary)
    {
        Id = Guid.NewGuid();
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary ?? GetRandomSummary();
        TemperatureF = 32 + (int)(TemperatureC / 0.5556);
    }

    public WeatherForecast() { }

    private static string GetRandomSummary()
    {
        string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

        return summaries[new Random().Next(summaries.Length)];
    }
}
