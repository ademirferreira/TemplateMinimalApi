using TemplateMinimalApi.Endpoints.Internal;
using TemplateMinimalApi.Models;

namespace TemplateMinimalApi.Endpoints;

public class WeatherForecastEnpoints : IEndpoints
{
    private const string BaseRoute = "api/weatherforecast";
    private const string Tag = "WeatherForecast";

    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet($"{BaseRoute}", GetWeatherForecast)
            .WithName("GetWeatherForecast")
            .WithTags(Tag)
            .Produces<IEnumerable<WeatherForecast>>(StatusCodes.Status200OK)
            .WithOpenApi()
            .WithTags(Tag)
            .AllowAnonymous();
    }


    internal static IResult GetWeatherForecast()
    {
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            )).ToArray();
        return Results.Ok(forecast);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
    }
}
