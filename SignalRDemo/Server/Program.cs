using System;
using Microsoft.AspNetCore.Hosting;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://*:8080","https://*:8081")
                .Build();

            host.Run();
        }
    }
}
