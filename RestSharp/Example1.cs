namespace RestSharpDemo;

using System.Text.Json;

public class Example1
{
    public static async Task Run()
    {
        var options = new RestClientOptions("https://localhost:5001/WeatherForecast")
        {
            ThrowOnAnyError = true,
            MaxTimeout = 1000,
        };
        var client = new RestClient(options, configureSerialization: s => s.UseSerializer(() => new SystemJsonSourceGenerationSerializer()));

        var request = new RestRequest()
            .AddQueryParameter("city", "jn");

        var jsonString = JsonSerializer.Serialize(new WeatherForecast(), SourceGenerationContext.Default.WeatherForecast);

        var response = await client.GetAsync<WeatherForecast[]>(request, CancellationToken.None);

        foreach (var weather in response ?? throw new NullReferenceException())
        {
            Console.WriteLine(weather?.Summary);
            Console.WriteLine(weather?.Date);
            Console.WriteLine(weather?.TemperatureC);
            Console.WriteLine(weather?.TemperatureF);
        }
    }
}
