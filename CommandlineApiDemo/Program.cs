using System;
using System.Collections;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CommandlineApiDemo
{
    class Program
    {

        static void Main(string[] args)
        {
            RunTest("(:参数分隔)", SimpletMethod, "-i:123", "-b");
            RunTest("(=参数分隔)", SimpletMethod, "-i=123", "-b");
            RunTest("(空格参数分隔)", SimpletMethod, "-i", "123", "-b");

            RunTest("custom", MiddlewarePipeline, "[just-say-hi]", "[just-say-hi2]", "-i=3", "-i=4");
            RunTest("nomal", MiddlewarePipeline, "-i=3", "-i=4", "-b", "--file-option=file.txt");
            RunTest("help", MiddlewarePipeline, "-h");
            RunTest("version", MiddlewarePipeline, "--version");
            RunTest("parse-error", MiddlewarePipeline, "[parse]", "--int-option=not-an-int", "--file-option=file.txt");
            RunTest("parse-right", MiddlewarePipeline, "[parse]", "-i=3", "-i=4", "-b", "--file-option=file.txt");
            RunTest("subcommand", MiddlewarePipeline, "subcommand");

            // corert编译后程序不支持dotnet core attach
            // RunTest("debug", MiddlewarePipeline, "[debug]", "-i=3", "-i=4");

            Console.WriteLine("\n任意键退出");
            Console.ReadLine();
        }

        static int SimpletMethod(params string[] args)
        {
            Console.WriteLine("test");

            var optionThatTakesInt = new Option(
               alias: "--int-option",
               description: "An option whose argument is parsed as an int",
               argument: new Argument<int>(defaultValue: 42)
               {
                   Arity = ArgumentArity.ExactlyOne
               },
               isHidden: default);
            optionThatTakesInt.AddAlias("-i");

            var optionThatTakesBool = new Option(
                "--bool-option",
                "An option whose argument is parsed as a bool",
                new Argument<bool>()
                {
                    Arity = ArgumentArity.ZeroOrOne
                });
            optionThatTakesInt.AddAlias("-b");

            Console.WriteLine("rootcommand ctor");
            var rootCommand = new RootCommandRT();
            rootCommand.Description = "My sample app";
            rootCommand.AddOption(optionThatTakesInt);
            rootCommand.AddOption(optionThatTakesBool);

            Console.WriteLine("rootcommand created");

            rootCommand.Handler = CommandHandler.Create(() =>
            {
                Console.WriteLine($"The value for --int-option is: ");
            });

            // rootCommand.InvokeAsync(args).GetAwaiter().GetResult();

            rootCommand.Handler = CommandHandler.Create<int, bool>((intOption, boolOption) =>
            {
                Console.WriteLine($"The value for --int-option is: {intOption}");
                Console.WriteLine($"The value for --bool-option is: {boolOption}");
            });

            // rootCommand.InvokeAsync(args).GetAwaiter().GetResult();

            var parse = new CommandLineBuilder(rootCommand).Build();
            var pr = parse.Parse(args);
            var context = new InvocationContext(pr);
            rootCommand.Handler.InvokeAsync(context).GetAwaiter().GetResult();


            return 0;
        }

        static int MiddlewarePipeline(params string[] args)
        {
            // Create some options and a parser
            var optionThatTakesInt = new Option(
               alias: "--int-option",
               description: "An option whose argument is parsed as an int[]",
               argument: new Argument<int[]>(defaultValue: new int[] { 1, 2, 3 })
               {
                   Arity = ArgumentArity.OneOrMore
               },
               isHidden: default);
            optionThatTakesInt.AddAlias("-i");

            var optionThatTakesBool = new Option(
                "--bool-option",
                "An option whose argument is parsed as a bool",
                new Argument<bool>()
                {
                    Arity = ArgumentArity.ZeroOrOne
                });
            optionThatTakesBool.AddAlias("-b");

            var optionThatTakesFileInfo = new Option(
                "--file-option",
                "An option whose argument is parsed as a FileInfo",
                new Argument<FileInfo>()
                {
                    Arity = ArgumentArity.ExactlyOne
                });


            // Add them to the root command
            // var rootCommand = new RootCommand();
            var rootCommand = new RootCommandRT();
            rootCommand.Description = "My sample app";
            rootCommand.AddOption(optionThatTakesInt);
            rootCommand.AddOption(optionThatTakesBool);
            rootCommand.AddOption(optionThatTakesFileInfo);

            rootCommand.Handler = CommandHandler.Create<int[], bool, FileInfo>((intOption, boolOption, fileOption) =>
            {
                foreach (var item in intOption)
                {
                    Console.WriteLine($"The value for --int-option is: {item}");
                }
                Console.WriteLine($"The value for --bool-option is: {boolOption}");
                Console.WriteLine($"The value for --file-option is: {fileOption?.FullName ?? "null"}");
            });

            // return rootCommand.InvokeAsync(new string[] { "-b" }).GetAwaiter().GetResult();
            // return rootCommand.InvokeAsync(new string[] { "-i:1", "-i:2", "-b" }).GetAwaiter().GetResult();

            var subcommand = new Command("subcommand");
            subcommand.AddOption(new Option(
                aliases: new string[] { "--i-op" },
                description: "An option whose argument is parsed as an int",
                argument: new Argument<int>()
                {
                    Arity = ArgumentArity.ExactlyOne
                }));
            subcommand.Handler = CommandHandler.Create<int>((iOption) =>
            {
                Console.WriteLine($"subcommand : {iOption}");
            });

            Console.WriteLine("inited");

            var builder = new CommandLineBuilder(rootCommand)
            .AddCommand(subcommand)
            .UseHelp()
            .UseVersionOption()
            .UseDebugDirective()
            .UseParseDirective()
            .UseMiddleware(async (context, next) =>
            {
                if (context.ParseResult.Directives.Contains("just-say-hi"))
                {
                    context.Console.Out.WriteLine("Hi!");
                    if (context.ParseResult.Directives.Contains("just-say-hi2"))
                    {
                        context.Console.Out.WriteLine("Hi2!");
                    }
                }
                else
                {
                    await next(context);
                }
            }).Build();

            Console.WriteLine("created");
            return builder.InvokeAsync(args).Result;
        }

        #region Run A Test Method

        delegate int TestMethod(params string[] args);
        static void RunTest(string name, TestMethod method, params string[] args)
        {
            var mainName = method.Method.Name;
            if (!string.IsNullOrEmpty(name))
            {
                string split = "-";
                mainName = method.Method.Name + split + name;
            }

            Console.WriteLine(mainName.PadRight(50, '='));

            method(args);

            Console.WriteLine();
        }

        #endregion
    }
}
