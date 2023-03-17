using System.Text.Json;
using System.Text.Json.Serialization;

string jsonString = @"{
    ""Date"": ""2019-08-01T00:00:00"",
    ""TemperatureCelsius"": 25,
    ""Summary"": ""Hot"",
    ""InnerData"": {
        ""City"": ""Jinan""
    }
}";

WeatherForecast? weatherForecast;

// Deserialize
{
    weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(jsonString, SourceGenerationContext.Default.WeatherForecast);
    Console.WriteLine($"Date={weatherForecast?.Date}");
    Console.WriteLine($"Date={weatherForecast?.InnerData?.City}");
    // output:
    //Date=8/1/2019 12:00:00 AM

    weatherForecast = JsonSerializer.Deserialize(jsonString, typeof(WeatherForecast), SourceGenerationContext.Default) as WeatherForecast;
    Console.WriteLine($"Date={weatherForecast?.Date}");
    Console.WriteLine($"Date={weatherForecast?.InnerData?.City}");
    // output:
    //Date=8/1/2019 12:00:00 AM

    var sourceGenOptions = new JsonSerializerOptions
    {
        TypeInfoResolver = SourceGenerationContext.Default
    };
    weatherForecast = JsonSerializer.Deserialize(jsonString, typeof(WeatherForecast), sourceGenOptions) as WeatherForecast;
    Console.WriteLine($"Date={weatherForecast?.Date}");
    Console.WriteLine($"Date={weatherForecast?.InnerData?.City}");
    // output:
    //Date=8/1/2019 12:00:00 AM
}

// Serialize
{
    jsonString = JsonSerializer.Serialize(weatherForecast!, SourceGenerationContext.Default.WeatherForecast);
    Console.WriteLine(jsonString);
    // output:
    //{"Date":"2019-08-01T00:00:00","TemperatureCelsius":25,"Summary":"Hot"}

    jsonString = JsonSerializer.Serialize(weatherForecast, typeof(WeatherForecast), SourceGenerationContext.Default);
    Console.WriteLine(jsonString);
    // output:
    //{"Date":"2019-08-01T00:00:00","TemperatureCelsius":25,"Summary":"Hot"}

    var sourceGenOptions = new JsonSerializerOptions
    {
        TypeInfoResolver = SourceGenerationContext.Default
    };
    jsonString = JsonSerializer.Serialize(weatherForecast, typeof(WeatherForecast), sourceGenOptions);
    Console.WriteLine(jsonString);
}

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureCelsius { get; set; }
    public string? Summary { get; set; }
    public InnerData? InnerData { get; set; }
}

public class InnerData
{
    public string? City { get; set; }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(WeatherForecast))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
