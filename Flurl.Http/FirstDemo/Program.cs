using System.Text.Json.Serialization;
using Flurl;
using Flurl.Http;

var response = await "https://api.muxiaoguo.cn"
    .AppendPathSegment("api/bad_or_luck")
    .SetQueryParams(new { Num = "11111111111" })
    .GetJsonAsync<MyResponse>();

Console.WriteLine(response?.Msg);
Console.WriteLine(response?.Data?.Analysis);
Console.WriteLine(response?.Data?.BadORluck);

public class MyResponse
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonPropertyName("msg")]
    public string? Msg { get; set; }

    [JsonPropertyName("data")]
    public Data? Data { get; set; }
}

public class Data
{
    [JsonPropertyName("fraction")]
    public string? Fraction { get; set; }

    [JsonPropertyName("analysis")]
    public string? Analysis { get; set; }

    [JsonPropertyName("badORluck")]
    public string? BadORluck { get; set; }
}
