namespace BuilderApiCoreRTDemo
{
    using System;
    using System.Threading.Tasks;
    using CommandLineUtils;
    using McMaster.Extensions.CommandLineUtils;

    internal class BuilderApi
    {
        internal static int Main(string[] args)
        {
            var app = new CommandLineApplication
            {
                Name = "BuilderApi",
                FullName = "BuilderApiCoreRTDemo.BuilderApi",
                Description = "BuilderApi版本",
                ExtendedHelpText = "描述完成"
            };

            app.HelpOption(template: "-h|--help", inherited: true);
            app.VersionOption(template: "--version", shortFormVersion: "短版本", longFormVersion: "长版本");
            app.Option(template: "-v|--verbose", description: "显示更多信息", optionType: CommandOptionType.NoValue, inherited: true);
            app.Command(name: "subcommand1", configuration: Subcommand1);
            app.Command(name: "delaycomand", configuration: a =>
            {
                a.OnExecuteAsync(token =>
                {
                    Console.WriteLine("delay start");
                    return Task.Delay(10000).ContinueWith(t => 0);
                });
            });

            app.OnExecute(() =>
            {
                return app.Execute(new string[] { "-h" });
            });

            return app.Execute(args);
        }

        private static void Subcommand1(CommandLineApplication app)
        {
            app.FullName = "subcommand1 full name";
            var countCommandArgument = app.Argument<int?>(name: "count", description: "个数", configuration: countArg => { }, multipleValues: false);
            var arg2CommandArgument = app.Argument<string>(name: "arg2", description: "arg2", configuration: countArg => { }, multipleValues: false);
            var optionSubject = app.Option<string>(template: "-s|--subject <SUBJECT>", description: "The subject", optionType: CommandOptionType.SingleValue);
            var optionRepeat = app.Option<int>(template: "-r|--repeat <N>", description: "Repeat", optionType: CommandOptionType.SingleValue);
            app.Command(name: "subcommand2", configuration: subapp => { });

            app.OnExecute(() =>
            {
                var options = app.GetOptions();
                var p = app.Parent;
                Console.WriteLine("Parent {0} {1}", p.Name, p.FullName);
                Console.WriteLine("Current {0} {1}", app.Name, app.FullName);
                foreach (var item in options)
                {
                    Console.WriteLine("{0} {1} {2} {3}", item.Description, item.SymbolName, item.ValueName, item.Value());
                }
                Console.WriteLine(countCommandArgument.ParsedValue);
                Console.WriteLine(optionSubject.ParsedValue);
                if (optionRepeat.HasValue())
                {
                    Console.WriteLine("repeat:{0}", optionRepeat.Value());
                }
                Console.WriteLine("fin");
                return 0;
            });
        }
    }

    internal class CommandManager
    {
        internal static int Main(string[] args)
        {
            var mainapp = new MainCommand("CommandManager", "BuilderApiCoreRTDemo.CommandManager", "CommandManager版本");
            return mainapp.Execute(args);
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var newargs = ArgumentParser.Parse(@" subcommand1  20  ""5 46"" -s  ""hello world"" -r:43 -v -a \'123\' ");

            // BuilderApi.Main(args);

            CommandManager.Main(newargs);
            CommandManager.Main(args);
        }
    }
}
