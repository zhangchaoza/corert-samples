namespace AdvancedAttributesCoreRTDemo
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    // using Microsoft.Extensions.DependencyInjection;
    // using Microsoft.Extensions.Logging;

    public static class ConfigureCommandLineApplication
    {
        public static int Execute<T>(Action<CommandLineApplication<T>> configure, params string[] args) where T : class
        {
            try
            {
                using (var app = new CommandLineApplication<T>())
                {
                    var services = new ServiceCollection()
                        .AddLogging(logging =>
                        {
                            logging.AddConsole();
                            logging.AddDebug();
                            logging.SetMinimumLevel(LogLevel.Information);
                        })
                        .BuildServiceProvider();

                    // app.Conventions.UseDefaultConventions();
                    // UseDefaultConventions list
                    app.Conventions
                        // .UseAttributes()
                        .SetAppNameFromEntryAssembly()
                        .SetRemainingArgsPropertyOnModel()
                        .SetSubcommandPropertyOnModel()
                        .SetParentPropertyOnModel()
                        .UseOnExecuteMethodFromModel()
                        .UseOnValidateMethodFromModel()
                        .UseOnValidationErrorMethodFromModel()
                        // .UseConstructorInjection()//不使用时AttrSubcommand必须包含无参构造函数
                        .UseDefaultHelpOption()
                        .UseCommandNameFromModelType()
                    ;

                    // 自定容器
                    app.Conventions
                        .UseConstructorInjection(services);

                    // // 自定Convention
                    // app.Conventions.AddConvention(new AttributeConvention());

                    // UseAttributes list
                    app.Conventions
                        .UseCommandAttribute()
                        .UseVersionOptionFromMemberAttribute()// 包含在UseAttributes中，不能重复设置
                        .UseVersionOptionAttribute()// 包含在UseAttributes中，不能重复设置
                        .UseHelpOptionAttribute()// 会覆盖UseDefaultHelpOption,包含在UseAttributes中，不能重复设置
                        .UseOptionAttributes()// 包含在UseAttributes中，不能重复设置
                        .UseArgumentAttributes()// 包含在UseAttributes中，不能重复设置
                        .UseSubcommandAttributes()// 包含在UseAttributes中，不能重复设置
                    ;

                    configure(app);
                    return app.Execute(args);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}{Environment.NewLine}{ex.StackTrace}");
                return 0;
            }
        }
    }
}
