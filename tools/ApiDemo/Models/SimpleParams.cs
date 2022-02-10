namespace ApiDemo.Models;

public class SimpleParams
{
    [JsonPropertyName("param1")]
    public int Param1 { get; set; }

    [JsonPropertyName("param2")]
    public string? Param2 { get; set; }
}