namespace AdvancedAttributesCoreRTDemo
{
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    [HelpOption("-h|--help|--get-help", Description = "显示帮助", Inherited = false)]
    // [VersionOption(version: "1.0.1")]
    [VersionOptionFromMember("--version", MemberName = nameof(VersionFromMember), Description = "显示版本号", Inherited = false)]
    [Subcommand(typeof(AttrSubcommand))]
    public class Primary
    {
        private readonly ILogger<Primary> logger;

        public Primary()
        {

        }

        public Primary(ILogger<Primary> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Arguments

        [Range(1, 3)]
        [Argument(order: 0, name: "intArg", description: "Int Argument")]
        public int? IntArg { get; set; }

        #endregion

        #region Options

        public string VersionFromMember => $"版本号：{ typeof(Primary).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}";

        /// <summary>
        /// 设置CommandOptionType.SingleValue测试GetParser
        /// </summary>
        [Option(template: "-b", description: "bool测试", optionType: CommandOptionType.SingleValue, Inherited = true)]
        public bool BoolValue { get; private set; }

        [Option(template: "-i", description: "int测试", optionType: CommandOptionType.SingleValue, Inherited = true)]
        public int Int { get; private set; }

        [Option(template: "--ni", description: "可空int测试", optionType: CommandOptionType.SingleValue, Inherited = true)]
        public int? NullableInt { get; private set; }

        /// <summary>
        /// 设置CommandOptionType.SingleValue测试GetParser
        /// </summary>
        [Option(template: "--nb", description: "可空bool测试", optionType: CommandOptionType.SingleValue, Inherited = true)]
        public bool? NullableBoolValue { get; private set; }

        [Option(template: "-e", description: "enum测试", optionType: CommandOptionType.SingleValue, Inherited = true)]
        public EnumOption EnumValue { get; private set; }

        [Option(template: "--ne", description: "可空enum测试", optionType: CommandOptionType.SingleValue, Inherited = true)]
        public EnumOption? NullableEnum { get; private set; }

        [Option(template: "-s", description: "string测试", optionType: CommandOptionType.SingleValue, Inherited = true)]
        public string StringValue { get; private set; }

        [Option(template: "--mi", description: "多值int测试", optionType: CommandOptionType.MultipleValue, Inherited = true)]
        public int[] MutiIntValues { get; private set; } = new int[0];

        #endregion Options

        private int OnExecute()
        {
            Console.WriteLine($"IntArg:{IntArg}");
            WriteInheritedOptionInfo();

            return 0;
        }

        internal void WriteInheritedOptionInfo()
        {
            Console.WriteLine("Int:{0},BoolValue:{1},EnumValue:{2},NullableInt:{3},NullableBoolValue:{4},NullableEnum:{5}",
                Int,
                BoolValue,
                EnumValue,
                NullableInt.HasValue ? NullableInt.ToString() : "null",
                NullableBoolValue.HasValue ? NullableBoolValue.ToString() : "null",
                NullableEnum.HasValue ? NullableEnum.ToString() : "null",
                StringValue);

            Console.WriteLine($"StringValue:{StringValue}");

            Console.WriteLine($"MutiIntValues{MutiIntValues.Length}:");
            for (int i = 0; i < MutiIntValues.Length; i++)
            {
                Console.WriteLine($"\t[{i}]:{MutiIntValues[i]}");
            }
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
