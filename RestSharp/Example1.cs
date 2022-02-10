namespace RestSharpDemo;

public class Example1
{
    public static async Task Run()
    {
        var options = new RestClientOptions("https://api.muxiaoguo.cn/api/bad_or_luck")
        {
            ThrowOnAnyError = true,
            Timeout = 1000
        };
        var client = new RestClient(options);

        var request = new RestRequest()
            .AddQueryParameter("Num", "11111111111");

        var response = await client.PostAsync<MyResponse>(request, CancellationToken.None);

        Console.WriteLine(response?.Msg);
        Console.WriteLine(response?.Data?.Analysis);
        Console.WriteLine(response?.Data?.BadORluck);
    }
}

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
