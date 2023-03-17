namespace RestSharpDemo;

using RestSharp.Serializers;
using System.Text.Json;
using System.Text.Json.Serialization;

public class WeatherForecast
{
    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("temperatureC")]
    public int TemperatureC { get; set; }

    [JsonPropertyName("temperatureF")]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }
}

public class SimpleParams
{
    [JsonPropertyName("param1")]
    public int Param1 { get; set; }

    [JsonPropertyName("param2")]
    public string? Param2 { get; set; }
}

public record SimpleParamsRecord
{
    [JsonPropertyName("param1")]
    public int Param1 { get; set; }

    [JsonPropertyName("param2")]
    public string? Param2 { get; set; }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(WeatherForecast))]
[JsonSerializable(typeof(WeatherForecast[]))]
[JsonSerializable(typeof(SimpleParams))]
[JsonSerializable(typeof(SimpleParamsRecord))]
[JsonSerializable(typeof(object))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}

public class SystemJsonSourceGenerationSerializer : IRestSerializer, ISerializer, IDeserializer
{
    readonly JsonSerializerOptions _options;

    public SystemJsonSourceGenerationSerializer() => _options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        TypeInfoResolver = SourceGenerationContext.Default
    };

    public SystemJsonSourceGenerationSerializer(JsonSerializerOptions options) => _options = options;

    public string? Serialize(object? obj) => obj == null ? null : JsonSerializer.Serialize(obj, typeof(object), _options);

    public string? Serialize(Parameter bodyParameter) => Serialize(bodyParameter.Value);

    public T? Deserialize<T>(RestResponse response) => (T)JsonSerializer.Deserialize(response.Content!, typeof(T), _options)!;

    public ContentType ContentType { get; set; } = ContentType.Json;

    public ISerializer Serializer => this;
    public IDeserializer Deserializer => this;
    public DataFormat DataFormat => DataFormat.Json;
    public string[] AcceptedContentTypes => ContentType.JsonAccept;
    public SupportsContentType SupportsContentType => contentType => contentType.Value.EndsWith("json", StringComparison.InvariantCultureIgnoreCase);
}
