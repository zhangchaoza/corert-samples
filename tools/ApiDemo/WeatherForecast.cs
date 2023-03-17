namespace WebApiDemo;

using System.Text.Json.Serialization;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}

public class SimpleParams
{
    [JsonPropertyName("param1")]
    public int Param1 { get; set; }

    [JsonPropertyName("param2")]
    public string? Param2 { get; set; }
}
