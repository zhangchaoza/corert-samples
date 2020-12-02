namespace AdvancedAttributesCoreRTDemo
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    public class AttrSubcommand
    {
        private readonly Primary _app;
        private readonly ILogger _logger;

        #region Arguments

        [Range(1, 3)]
        [Argument(order: 0, name: "intArg", description: "Int Argument")]
        public int? IntArg { get; set; }

        #endregion Arguments

        /// <summary>
        /// 不使用app.Conventions.UseConstructorInjection时AttrSubcommand必须包含无参构造函数
        /// </summary>
        public AttrSubcommand()
        {
        }

        public AttrSubcommand(Primary app, ILogger<AttrSubcommand> logger)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private int OnExecute()
        {
            Console.WriteLine("Subcommand OnExecute");

            Console.WriteLine($"IntArg:{IntArg}");
            _app?.WriteInheritedOptionInfo();

            return 0;
        }
    }
}
