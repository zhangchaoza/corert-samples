namespace AdvancedAttributesCoreRTDemo
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using McMaster.Extensions.CommandLineUtils.Conventions;
    // using Microsoft.Extensions.DependencyInjection;
    // using Microsoft.Extensions.Logging;

    public static class ConfigureCommandLineApplication
    {
        public static int Execute<T>(Action<CommandLineApplication<T>> configure, params string[] args) where T : class
        {
            try
            {
                using (var app = new CommandLineApplication<T>(throwOnUnexpectedArg: true))
                {
                    // var services = new ServiceCollection()
                    //     .AddLogging(logging =>
                    //     {
                    //         logging.AddConsole();
                    //         logging.AddDebug();
                    //         logging.SetMinimumLevel(LogLevel.Debug);
                    //     })
                    //     .BuildServiceProvider();


                    // app.Conventions.UseDefaultConventions();
                    // // UseDefaultConventions list
                    app.Conventions
                        .UseAttributes()
                        .SetAppNameFromEntryAssembly()
                        // .SetRemainingArgsPropertyOnModel()
                        .SetSubcommandPropertyOnModel()
                        // .SetParentPropertyOnModel()
                        .UseOnExecuteMethodFromModel()
                        // .UseOnValidateMethodFromModel()
                        // .UseOnValidationErrorMethodFromModel()
                        // .UseConstructorInjection()
                        .UseDefaultHelpOption()
                        .UseCommandNameFromModelType()
                        ;

                    // app.Conventions.UseConstructorInjection(services);
                    // app.Conventions.UseConstructorInjection();

                    // app.Conventions
                    // .AddConvention(new AttributeConvention())
                    // .UseCommandAttribute()
                    // .UseVersionOptionFromMemberAttribute()
                    // .UseVersionOptionAttribute()
                    // .UseHelpOptionAttribute()
                    // .UseOptionAttributes()
                    // .UseArgumentAttributes()
                    // .UseSubcommandAttributes()
                    // .SetAppNameFromEntryAssembly()
                    // .SetRemainingArgsPropertyOnModel()
                    // .SetSubcommandPropertyOnModel()
                    // .SetParentPropertyOnModel()
                    // .UseOnExecuteMethodFromModel()
                    // .UseOnValidateMethodFromModel()
                    // .UseOnValidationErrorMethodFromModel()
                    // .UseDefaultHelpOption();

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
