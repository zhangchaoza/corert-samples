namespace AdvancedAttributesCoreRTDemo
{
    using McMaster.Extensions.CommandLineUtils;
    using System;
    // using System.ComponentModel.DataAnnotations;

    [HelpOption("-h|--help", Description = "显示帮助", Inherited = false)]
    public class SubCommand1
    {
        private readonly Primary _app;

        /// <summary>
        /// 不使用app.Conventions.UseConstructorInjection时SubCommand1必须包含无参构造函数
        /// </summary>
        public SubCommand1()
        {
        }

        public SubCommand1(Primary app)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
        }

        public Primary Parent { get; set; }

        #region Arguments

        // [Range(1, 30)]
        [Argument(order: 0, name: "Count", description: "计数")]
        public int? Count { get; set; }

        #endregion Arguments

        #region Options

        [Option(template: "-so", description: "选项1", optionType: CommandOptionType.SingleValue, Inherited = false)]
        public string SingleOption { get; private set; }

        [Option(template: "-mo", description: "选项1", optionType: CommandOptionType.MultipleValue, Inherited = false)]
        public int[] MultipleOption { get; private set; }

        #endregion Options

        private int OnExecute()
        {
            Console.WriteLine("SubCommand1.OnExecute");

            if (Count.HasValue)
            {
                Console.WriteLine($"计数：{Count}");
            }
            else
            {
                Console.WriteLine("没有计数,默认3");
            }

            for (int i = 0; i < Count; i++)
            {
                Console.WriteLine($"so:{SingleOption},mo:{string.Join(",", MultipleOption)}");
            }

            _app?.WriteInheritedOptionInfo();

            return 0;
        }
    }
}
