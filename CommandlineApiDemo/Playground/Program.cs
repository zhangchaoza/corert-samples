namespace CommandlineApiDemo
{
    using System;
    using System.Collections.Generic;
    using System.CommandLine;
    using System.CommandLine.Builder;
    using System.CommandLine.Help;
    using System.CommandLine.Invocation;
    using System.CommandLine.IO;
    using System.CommandLine.Parsing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.CommandLine.NamingConventionBinder;


    internal class Program
    {
        private static void Main(string[] args)
        {
            RunTest("(:参数分隔)", SimpletMethod, "-i:123", "-b");
            RunTest("(=参数分隔)", SimpletMethod, "-i=123", "-b");
            RunTest("(空格参数分隔)", SimpletMethod, "-i", "123", "-b");

            RunTest("custom", MiddlewarePipeline, "[command:12:23:34]", "[just-say-hi]", "[just-say-hi2]", "playground_arg", "abc", "100", "-i=3", "-i=4");
            RunTest("nomal", MiddlewarePipeline, "playground_arg", "abc", "100", "-i=3", "-i=4", "-b", "--file-option=file.txt");
            RunTest("ResponseFileHandling", MiddlewarePipeline, "playground_arg", "abc", "100", "-i=3", "-i=4", "-b", "--file-option=file.txt", "@response_line.txt");
            RunTest("ResponseFileHandling", MiddlewarePipeline, "playground_arg", "abc", "100", "-i=3", "-i=4", "-b", "--file-option=file.txt", "@response_space.txt");
            RunTest("help", MiddlewarePipeline, "-h");
            RunTest("version", MiddlewarePipeline, "--version");
            RunTest("parse-error", MiddlewarePipeline, "[parse]", "--int-option=not-an-int", "--file-option=file.txt");
            RunTest("parse-error report", MiddlewarePipeline, "--int-option=not-an-int", "--file-option=file.txt");
            RunTest("parse-right", MiddlewarePipeline, "[parse]", "playground_arg", "abc", "100", "-i=3", "-i=4", "-b", "--file-option=file.txt");
            RunTest("Simple result", MiddlewarePipeline, "[Simple]", "playground_arg", "abc", "100", "-i=3", "-i=4", "-b", "--file-option=file.txt");

            RunTest("subcommand help", MiddlewarePipeline, "sub2", "-h");
            RunTest("subcommand", MiddlewarePipeline, "playground_arg", "abc", "100", "subcommand", "10", "--i-op=10");
            RunTest("subcommand Alias", MiddlewarePipeline, "playground_arg", "abc", "100", "sub", "qwe", "--i-op=10");
            RunTest("subcommand Exception", MiddlewarePipeline, "playground_arg", "abc", "100", "errorcommand");
            RunTest("subcommand Typo Corrections", MiddlewarePipeline, "playground_arg", "abc", "100", "--file-options=1");
            // RunTest("suggest1", MiddlewarePipeline, "[suggest]", "h");// suggest指令参数缺省为0
            // RunTest("suggest2", MiddlewarePipeline, "[suggest]", "wor");
            // RunTest("suggest3", MiddlewarePipeline, "[suggest]", "suggest");
            // RunTest("suggest4", MiddlewarePipeline, "[suggest:5]", "abc subs ge");// suggest参数将在字符串索引处取单词

            // corert编译后程序不支持dotnet core attach
            // RunTest("debug", MiddlewarePipeline, "[debug]", "-i=3", "-i=4");

            // Console.WriteLine("\n任意键退出");
            // Console.ReadLine();
        }

        private static int SimpletMethod(params string[] args)
        {
            Console.WriteLine("test");

            var optionThatTakesInt = new Option<int>(
                name: "--int-option",
                description: "An option whose argument is parsed as an int",
                getDefaultValue: () => 42)
            {
                Arity = ArgumentArity.ExactlyOne,
                IsHidden = false
            };
            optionThatTakesInt.AddAlias("-i");

            var optionThatTakesBool = new Option<bool>(
                "--bool-option",
                "An option whose argument is parsed as a bool")
            {
                Arity = ArgumentArity.ZeroOrOne,
            };
            optionThatTakesInt.AddAlias("-b");

            Console.WriteLine("rootcommand ctor");
            var rootCommand = new RootCommand();
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
            var context = new InvocationContext(pr, new SystemConsole());
            rootCommand.Handler.InvokeAsync(context).GetAwaiter().GetResult();

            return 0;
        }

        private static int MiddlewarePipeline(params string[] args)
        {
            // Create some options and a parser
            var optionThatTakesInt = new Option<int[]>(
                name: "--int-option",
                description: "An option whose argument is parsed as an int[]",
                getDefaultValue: () => new int[] { 1, 2, 3 })
            {
                Arity = ArgumentArity.OneOrMore,
                IsHidden = false
            };
            optionThatTakesInt.AddAlias("-i");

            // optionThatTakesInt.
            var optionThatTakesBool = new Option<bool>(
                name: "--bool-option",
                description: "An option whose argument is parsed as a bool")
            {
                Arity = ArgumentArity.ZeroOrOne,
            };
            optionThatTakesBool.AddAlias("-b");

            var optionThatTakesFileInfo = new Option<FileInfo>(
                name: "--file-option",
                description: "An option whose argument is parsed as a FileInfo")
            {
                Arity = ArgumentArity.ExactlyOne
            };

            // Add them to the root command
            // var rootCommand = new RootCommand();
            var rootCommand = new RootCommand("My sample app");
            rootCommand.AddAlias("pgapp");

            // rootCommand.AddOption(optionThatTakesInt);
            // rootCommand.AddOption(optionThatTakesBool);
            // rootCommand.AddOption(optionThatTakesFileInfo);

            // ※ Argument顺序重要
            Argument argument = new Argument()
            {
                Name = "playground",
                ValueType = typeof(string),
                Description = "默认参数"
            };
            // Suggest信息会显示在help中,并且会覆盖argument.name
            argument.AddCompletions(new string[]
            {
                "Suggest1",
                "Suggest2",
                "substring suggest"
            });
            // rootCommand.AddArgument(argument);
            Argument<string> argument1 = new Argument<string>("arg1")
            {
                Description = "string参数"
            };
            argument1.Completions.Add(new SimpleSuggestSource());
            // rootCommand.AddArgument(argument1);
            Argument<int> argument2 = new Argument<int>("arg2");
            argument2.AddCompletions(c =>
            {
                // Console.WriteLine($"textToMatch:\u001b[31m{textToMatch}\u001b[0m");
                return new string[]
                {
                    "world"
                };
            });
            // rootCommand.AddArgument(argument2);

            // ※ handler中可以乱序
            rootCommand.Handler = CommandHandler.Create<InvocationContext, string, int, string, int[], bool, FileInfo>((context, playground, arg2, arg1, intOption, boolOption, fileOption) =>
            {
                Console.WriteLine("Arguments:");
                Console.WriteLine($"\tplayground:{playground}");
                Console.WriteLine($"\targ1:{arg1}");
                Console.WriteLine($"\targ2:{arg2}");

                Console.WriteLine("Options:");
                foreach (var item in intOption)
                {
                    Console.WriteLine($"\tThe value for --int-option is: {item}");
                }
                Console.WriteLine($"\tThe value for --bool-option is: {boolOption}");
                Console.WriteLine($"\tThe value for --file-option is: {fileOption?.FullName ?? "null"}");
            });

            // rootCommand.Handler = CommandHandler.Create((IConsole console, CancellationToken token) =>
            // {
            //     return 0;
            // });

            // return rootCommand.InvokeAsync(new string[] { "-b" }).GetAwaiter().GetResult();
            // return rootCommand.InvokeAsync(new string[] { "-i:1", "-i:2", "-b" }).GetAwaiter().GetResult();

            // Console.WriteLine("inited");

            rootCommand.AddCommand(BuildSubcommand());
            rootCommand.AddCommand(BuildErrorSubcommand());
            rootCommand.AddGlobalOption(new Option("--global", "global option sample"));// 全局选项，适用到所有子命令
            rootCommand.AddOption(optionThatTakesInt);
            rootCommand.AddOption(optionThatTakesBool);
            rootCommand.AddOption(optionThatTakesFileInfo);
            rootCommand.AddArgument(argument);
            rootCommand.AddArgument(argument1);
            rootCommand.AddArgument(argument2);

            var builder = new CommandLineBuilder(rootCommand)

                // .EnablePositionalOptions(value: true)/* 无用 */

                .EnablePosixBundling(value: true)/*  对无值option生效，-b */

                // .ParseResponseFileAs(responseFileHandling: ResponseFileHandling.ParseArgsAsLineSeparated) /*  添加@起始参数，从文件读取命令 */
                .ParseResponseFileAs(responseFileHandling: ResponseFileHandling.ParseArgsAsSpaceSeparated)

                // .UseDefaults()
                ////.UseVersionOption()
                .UseHelp()
                ////.UseEnvironmentVariableDirective()
                ////.UseParseDirective()
                ////.UseDebugDirective()
                ////.UseSuggestDirective()
                ////.RegisterWithDotnetSuggest()
                ////.UseTypoCorrections()
                ////.UseParseErrorReporting()
                ////.UseExceptionHandler()
                ////.CancelOnProcessTermination()

                // /* 自定义帮助信息输出类 */
                // .UseHelpBuilder(context =>
                // {
                //     return new HelpBuilder(new LocalizationResources());
                // })

                .UseVersionOption() // 版本号选项，--version

                .CancelOnProcessTermination() // 控制台ctrl+c取消事件

                // 设置console
                .ConfigureConsole(context =>
                {
                    return context.Console;
                })
                .RegisterWithDotnetSuggest() // 需要安装dotnet-suggest

                .UseDebugDirective() // 使用[debug]指令

                // 命令异常处理
                .UseExceptionHandler((ex, context) =>
                {
                    context.Console.Error.WriteLine($"ExceptionHandler<{ex.GetType()}>:\u001b[31m{ex.Message}\u001b[0m");
                })

                // | lv  | value | MiddlewareOrderInternal      | MiddlewareOrder  |
                // | --- | ----- | ---------------------------- | ---------------- |
                // | 1   | -4000 | Startup                      |                  |
                // | 2   | -3000 | ExceptionHandler             |                  |
                // | 3   | -2600 | EnvironmentVariableDirective |                  |
                // | 4   | -2500 | ConfigureConsole             |                  |
                // | 5   | -2400 | RegisterWithDotnetSuggest    |                  |
                // | 6   | -2300 | DebugDirective               |                  |
                // | 7   | -2200 | ParseDirective               |                  |
                // | 8   | -2000 | SuggestDirective             | ExceptionHandler |
                // | 9   | -1900 | TypoCorrection               |                  |
                // | 10  | -1200 | VersionOption                |                  |
                // | 11  | -1100 | HelpOption                   |                  |
                // | 12  | -1000 |                              | Configuration    |
                // | 13  | 0     |                              | Default          |
                // | 14  | 1000  | ParseErrorReporting          | ErrorReporting   |
                // .UseHelp()/* help中间件优先级为11级 */
                // .UseHelpBuilder(context => new HelpBuilder(context.Console))

                // 普通中间件优先级为13级
                .AddMiddleware(context =>
                {
                    // 默认执行后续中间件
                    if (context.ParseResult.Directives.TryGetValues("command", out IReadOnlyList<string> value))
                    {
                        Console.WriteLine(string.Join(',', value.ToArray()));
                    }
                })
                .AddMiddleware(async (context, next) =>
                {
                    // 选择执行后续中间件
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
                })
                .AddMiddleware(async (context, next) =>
                {
                    if (context.ParseResult.Directives.Contains("Simple"))
                    {
                        context.InvocationResult = new SimpleResult();
                    }
                    await next(context);
                })
                .UseParseDirective()
                // 当解析失败直接返回，不会执行设置的中间件
                .UseParseErrorReporting()

                // 拼写错误提示
                // 0.3.0-alpha.19317.1 bug command有参数时异常 https://github.com/dotnet/command-line-api/issues/578
                .UseTypoCorrections()

                // 显示argument添加的suggest，并通过指令参数搜索
                // 指令参数表示搜索的字符串索引处单个单词
                // 指令参数缺省为0
                .UseSuggestDirective()
                .Build();

            // Console.WriteLine("created");
            return builder.InvokeAsync(args).Result;
        }

        private static Command BuildSubcommand()
        {
            // command缺少description时在help中不出现
            var subcommand = new Command("subcommand", "a subcommand");

            subcommand.AddAlias("sub");
            subcommand.AddAlias("sub2");

            subcommand.AddArgument(new Argument<string>("sub_arg1")
            {
                Description = "sub string参数"
            });

            subcommand.AddOption(new Option<int>(
                aliases: new string[] { "--i-op" },
                description: "An option whose argument is parsed as an int")
            {
                Arity = ArgumentArity.ExactlyOne
            });
            subcommand.Handler = CommandHandler.Create<int, string, string, string>((iop, playground, arg1, sub_arg1) =>
            {
                Console.WriteLine("subcommand Arguments:");
                Console.WriteLine($"\tplayground:{playground}");
                Console.WriteLine($"\targ1:{arg1}");
                Console.WriteLine($"\tsub_arg1:{sub_arg1}");

                Console.WriteLine("subcommand Options:");
                Console.WriteLine($"\t--i-op: {iop}");
            });

            return subcommand;
        }

        private static Command BuildErrorSubcommand()
        {
            // command缺少description时在help中不出现
            var subcommand = new Command("errorcommand", "a error subcommand");

            subcommand.Handler = CommandHandler.Create(() =>
            {
                return Task.FromException(new NotImplementedException());
            });

            return subcommand;
        }

        #region Run A Test Method

        private delegate int TestMethod(params string[] args);

        private static void RunTest(string name, TestMethod method, params string[] args)
        {
            var mainName = method.Method.Name;
            if (!string.IsNullOrEmpty(name))
            {
                string split = "-";
                mainName = method.Method.Name + split + name;
            }

            Console.WriteLine(mainName.PadRight(50, '='));

            var result = method(args);

            Console.WriteLine();
            Console.WriteLine($"exit code:{result}");
        }

        #endregion Run A Test Method
    }
}
