namespace RestSharpDemo;

using System.Net;
using System.Text;
using System.Text.Json;

public class Example2
{
    public static async Task Run()
    {
        await PostJson("https://localhost:5001/WeatherForecast", request =>
        {
            request.AddHeader("Content-Type", "application/json");
        }, CancellationToken.None, repeatTimes: 3);

        await PostJson("https://localhost:5001/WeatherForecast/post2", request =>
        {
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new SimpleParams
            {
                Param1 = 1,
                Param2 = "abc",
            });
        }, CancellationToken.None, repeatTimes: 3);

        // use Anonymous type,need rd.xml
        await PostJson("https://localhost:5001/WeatherForecast/post2", request =>
        {
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new SimpleParamsRecord
            {
                Param1 = 1,
                Param2 = "abc",
            });
        }, CancellationToken.None, repeatTimes: 3);

        // <>f__AnonymousType0 failed
        // await PostJson("https://localhost:5001/WeatherForecast/post2", request =>
        // {
        //     request.AddHeader("Content-Type", "application/json");
        //     request.AddJsonBody(new
        //     {
        //         Param1 = 1,
        //         Param2 = "abc",
        //         Param3 = true,
        //     });
        // }, CancellationToken.None, repeatTimes: 3);
    }

    protected static async Task<JsonElement?> PostJson(string url, Action<RestRequest> config, CancellationToken token, uint repeatTimes = 0)
    {
        var options = new RestClientOptions(url)
        {
            ThrowOnAnyError = true,
            MaxTimeout = 1000,
            Proxy = System.Net.Http.HttpClient.DefaultProxy
        };
        var client = new RestClient(options, configureSerialization: s => s.UseSerializer(() => new SystemJsonSourceGenerationSerializer()));

        var request = new RestRequest();
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        config?.Invoke(request);

        uint times = 0;
repeat:

        times++;
        var response = await client.ExecutePostAsync(request);
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("===============API===============");
        sb.AppendFormat("URL:{0}", url);
        sb.AppendLine();
        sb.AppendLine(request.Parameters.TryFind("Content-Type")?.Value?.ToString());
        foreach (var p in request.Parameters)
        {
            sb.AppendFormat("\t{0}:{1}", p.Name, p.Value);
            sb.AppendLine();
        }
        sb.AppendFormat("StatusCode:{0}", (int)response.StatusCode);
        sb.AppendLine();

        try
        {
            if (null != response.ErrorException)
            {
                sb.AppendLine("Respone Error");
                sb.AppendLine($"{response.ErrorException.Message}");
                sb.AppendLine($"{response.ErrorException.StackTrace}");
                throw response.ErrorException;
            }
            if (response.StatusCode != HttpStatusCode.OK)
            {
                sb.AppendLine("Respone result is not OK");
                sb.AppendLine($"{response.StatusCode}");
                sb.AppendLine(response.Content);
                //throw new Exception($"[{(int)response.StatusCode}]服务器上发生了一般错误");
                //throw new Exception($"[{(int)response.StatusCode}]Call [{client.BaseUrl.AbsolutePath}] failed!");
                throw new Exception($"[{(int)response.StatusCode}]Call [{url}] failed!{Environment.NewLine}{response.Content}");
            }

            // 接口没有内容返回
            if (response.ContentLength == 0)
            {
                return null;
            }
            sb.AppendLine(response.Content);
            var result = JsonSerializer.Deserialize<JsonElement>(response.Content ?? throw new NullReferenceException());

            int status = -1;
            string info = string.Empty;
            if (!result.TryGetProperty("status", out var sjt))
            {
                if (!result.TryGetProperty("code", out sjt))
                {
                }
                else
                {
                    status = sjt.GetInt32();
                }
            }
            else
            {
                status = sjt.GetInt32();
            }
            if (result.TryGetProperty("info", out var ijt))
            {
                info = ijt.GetString() ?? string.Empty;
            }

            sb.AppendFormat("RESPONSE:{0}", $"[{status}]{info}");
            sb.AppendLine();
            return result;
        }
        catch (System.Net.WebException ex)
        {
            if (repeatTimes > 0)
            {
                sb.AppendLine($"call times {times}/{repeatTimes}");
                if (times < repeatTimes)
                {
                    if (WebExceptionStatus.Timeout != ex.Status)
                    {
                        await Task.Delay(3000, token).ConfigureAwait(false);
                    }
                    goto repeat;
                }
            }
            throw;
        }
        finally
        {
            sb.Append("========================================");
            Console.WriteLine(sb.ToString());
        }
    }
}


