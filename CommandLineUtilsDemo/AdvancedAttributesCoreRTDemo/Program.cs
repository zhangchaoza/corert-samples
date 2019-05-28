using System;
using System.Linq;
using System.Reflection;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Abstractions;

namespace AdvancedAttributesCoreRTDemo
{

    internal class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("start AdvancedAttributesCoreRTDemo");
            // TemporarySolution();

            Console.WriteLine("1.help");
            ConfigureCommandLineApplication.Execute<Primary>(Constant.ConfigurePrimary, new string[] { "-h" });
            Console.WriteLine("\n2.Empty");
            ConfigureCommandLineApplication.Execute<Primary>(Constant.ConfigurePrimary, new string[] { });
            Console.WriteLine("\n3.Has Values");
            ConfigureCommandLineApplication.Execute<Primary>(Constant.ConfigurePrimary, new string[] {
                "2",
                "-i:10",
                "-b:true",
                "--ni:11",
                "--nb:true",
                "-e:7",
                "--ne:5" ,
                "-s:CommandLineUtils in CoreRT",
                "--mi:1",
                "--mi:2"});
            Console.WriteLine("\n4.Range");
            ConfigureCommandLineApplication.Execute<Primary>(Constant.ConfigurePrimary, new string[] { "100" });
            Console.WriteLine("\n5.Subcommand");
            ConfigureCommandLineApplication.Execute<Primary>(Constant.ConfigurePrimary, new string[] {
                "attr-subcommand" ,
                "2",
                "-i:10",
                "-b:true",
                "--ni:11",
                "--nb:true",
                "-e:7",
                "--ne:5" ,
                "-s:CommandLineUtils in CoreRT",
                "--mi:1",
                "--mi:2"});

            return 0;
        }

        /// <summary>
        /// 临时解决ValueParserProvider.GetParser及RD.xml方法重载问题
        /// </summary>
        public static void TemporarySolution()
        {
            var app = new CommandLineApplication<Primary>();
            _ = app.ValueParsers.GetParser<int>();
            _ = app.ValueParsers.GetParser<bool>();
            _ = app.ValueParsers.GetParser<int?>();
            _ = app.ValueParsers.GetParser<bool?>();
            _ = app.ValueParsers.GetParser<string>();
            _ = app.ValueParsers.GetParser<EnumOption>();
            _ = app.ValueParsers.GetParser<EnumOption?>();

            // // 由于corert未支持方法重载，配置rd.xml会异常
            // {
            //     var s_GetParserGeneric = typeof(ValueParserProvider).GetTypeInfo()
            //         .GetMethods(BindingFlags.Instance | BindingFlags.Public)
            //         .Single(m => m.Name == "GetParser" && m.IsGenericMethod);
            //     var method = s_GetParserGeneric.MakeGenericMethod(typeof(int));

            //     Console.WriteLine(method);
            // }
        }
    }
}
