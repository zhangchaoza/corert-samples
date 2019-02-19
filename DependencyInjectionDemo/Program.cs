namespace DIDemo
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var configRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddIniFile("config.ini", optional: true, reloadOnChange: true)
                .Build();

            ServiceCollection services = new ServiceCollection();
            var provider = services
            .AddOptions()
            .AddTransient<TestOption>()
            .Configure<TestOption>("test", op =>
            {
                op.Name = "haha1";
                op.Num = 9;
            })
            .Configure<TestOption>("test2", configRoot)
            .BuildServiceProvider();

            DisplayOption(provider.GetRequiredService<TestOption>());
            DisplayOption(provider.GetService<IOptionsSnapshot<TestOption>>().Get("test"));
            DisplayOption(provider.GetService<IOptionsSnapshot<TestOption>>().Get("test2"));
        }

        private static void DisplayOption(TestOption option)
        {
            Console.WriteLine($"{option.Name},{option.Num}");
        }
    }
}
