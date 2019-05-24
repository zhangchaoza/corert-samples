namespace AdvancedAttributesCoreRTDemo
{
    using McMaster.Extensions.CommandLineUtils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    [HelpOption("-h|--help", Description = "显示帮助", Inherited = false)]
    [VersionOptionFromMember("--version", MemberName = nameof(Version), Description = "显示版本号", Inherited = false)]
    [Subcommand(typeof(Subcommand))]
    public class Primary
    {

        public Primary()
        {

        }


        #region Arguments

        [Range(1, 3)]
        [Argument(order: 0, name: "intArg", description: "Int Argument")]
        public int? IntArg { get; set; }

        #endregion

        #region Options

        public string Version => $"版本号：{ typeof(Primary).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}";

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
            WriteInfo();

            return 0;
        }

        internal void WriteInfo()
        {
            Console.WriteLine($"IntArg:{IntArg}");
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

    public class Subcommand
    {
        private readonly Primary _app;

        #region Arguments

        [Range(1, 3)]
        [Argument(order: 0, name: "intArg", description: "Int Argument")]
        public int? IntArg { get; set; }

        #endregion

        public Subcommand()
        {
        }

        public Subcommand(Primary app)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
        }

        private int OnExecute()
        {
            Console.WriteLine("Subcommand OnExecute");
            _app.WriteInfo();

            return 0;
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
