namespace HostingDemo
{
    using System;
    using System.CommandLine.Builder;
    using System.CommandLine.Hosting;
    using System.Threading.Tasks;
    using System.CommandLine;
    using System.CommandLine.Parsing;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System.CommandLine.NamingConventionBinder;

    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var builder = new CommandLineBuilder()
                .UseHost(host =>
                {
                    host.ConfigureServices((context, services) =>
                    {
                        // services.AddTransient(typeof(ILoggerFactory), typeof(LoggerFactory));
                        // services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
                        services.AddLogging(logging => logging
                            .AddConsole()
                            .AddDebug()
                            .SetMinimumLevel(LogLevel.Trace));
                    });
                })
                .UseDebugDirective()
                .UseHelp()
                .UseTypoCorrections()
                .UseAttributedCommands()
                .UseSuggestDirective();

            var parser = builder.Build();
            var parseResult = parser.Parse("test -b -a \"parameter a\"");
            var invoke = await parseResult.InvokeAsync();
            return invoke;
        }
    }

    public static class BuilderExtensions
    {
        public static CommandLineBuilder UseAttributedCommands(this CommandLineBuilder source)
        {
            var method = typeof(BuilderExtensions).GetMethod(nameof(Test), BindingFlags.Static | BindingFlags.NonPublic);
            var testCommand = new Command("test");
            testCommand.AddOption(new Option<string>("-a") { Arity = ArgumentArity.ExactlyOne });
            testCommand.AddOption(new Option<bool>("-b") { Arity = ArgumentArity.Zero });
            testCommand.Handler = CommandHandler.Create(method, null);
            source.Command.AddCommand(testCommand);

            return source;
        }

        private static void Test(IHost host, string a, bool b)
        {
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("LogInformation");
            logger.LogDebug("just a test really");
            Console.WriteLine(a);
            Console.WriteLine(b);
        }
    }
}
