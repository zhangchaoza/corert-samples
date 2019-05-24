namespace AttributesCoreRTDemo
{
    using McMaster.Extensions.CommandLineUtils;
    using System;
    using System.Reflection;

    [HelpOption("-h|--help", Description = "显示帮助", Inherited = false)]
    [VersionOptionFromMember("--version", MemberName = nameof(Version), Description = "显示版本号", Inherited = false)]
    public class Primary
    {
        // [Range(1, 3)]
        // [Argument(0)]
        // public int? Option { get; set; }

        // public string[] RemainingArgs { get; set; }

        public string Version => $"版本号：{ typeof(Primary).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}";

        #region Options

        [Option(template: "-v|--verbose", description: "输出详细信息", optionType: CommandOptionType.NoValue, Inherited = true)]
        public bool IsVerbose { get; private set; }

        [Option(template: "-i", description: "可空int测试", optionType: CommandOptionType.SingleValue, Inherited = false)]
        public int? NullableInt { get; private set; }

        // [Option(template: "-e", description: "可空enum测试", optionType: CommandOptionType.SingleValue, Inherited = false)]
        // public EnumOption? NullableEnum { get; private set; }

        #endregion Options

        private int OnExecute()
        {
            // Console.WriteLine($"IsVerbose:{IsVerbose},NullableInt:{NullableInt},NullableEnum:{NullableEnum}");
            Console.WriteLine($"IsVerbose:{IsVerbose},NullableInt:{NullableInt}");

            return 1;
        }
    }

    [Flags]
    public enum EnumOption
    {
        A = 1,
        B = 2,
        C = 4
    }

}
