using RestSharp;

var options = new RestClientOptions("https://api.muxiaoguo.cn/api/bad_or_luck")
{
    ThrowOnAnyError = true,
    Timeout = 1000
};
var client = new RestClient(options);

var request = new RestRequest()
    .AddQueryParameter("Num", "11111111111");

var response = await client.PostAsync<MyResponse>(request, CancellationToken.None);

Console.WriteLine(response?.msg);
Console.WriteLine(response?.data?.analysis);
Console.WriteLine(response?.data?.badORluck);

public class MyResponse
{
    public int code { get; set; }
    public string? msg { get; set; }
    public data? data { get; set; }
}

public class data
{
    public string? fraction { get; set; }
    public string? analysis { get; set; }
    public string? badORluck { get; set; }
}
