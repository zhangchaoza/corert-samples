using Microsoft.AspNetCore.Hosting;

namespace Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://*:8080", "https://*:8081")
                .Build();

            host.Run();
        }
    }
}
