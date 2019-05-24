using System;
using System.Linq;
using System.Reflection;
using McMaster.Extensions.CommandLineUtils;
using McMaster.Extensions.CommandLineUtils.Abstractions;

namespace AttributesCoreRTDemo
{

    internal class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("start");
            // {
            //     var app = new CommandLineApplication<Primary>();
            //     var a = app.ValueParsers.GetParser<int>();
            //     Console.WriteLine(a.TargetType);
            // }
            {
                var app = new CommandLineApplication<Primary>();
                var typeInfo = typeof(int?).GetTypeInfo();
                Console.WriteLine($"GetTypeInfo:{typeInfo}");

                var a = app.ValueParsers.GetParser<int?>();
            }

            // 由于corert为支持方法重载，
            {
                var s_GetParserGeneric = typeof(ValueParserProvider).GetTypeInfo()
                    .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Single(m => m.Name == "GetParser" && m.IsGenericMethod);
                var method = s_GetParserGeneric.MakeGenericMethod(typeof(int));

                Console.WriteLine(method);
            }

            // {
            //     var app = new CommandLineApplication<Primary>();
            //     var typeInfo = typeof(int?).GetTypeInfo();
            //     Console.WriteLine($"GetTypeInfo:{typeInfo}");

            //     var a = app.ValueParsers.GetParser(typeof(int));
            // }

            // CommandLineApplication.Execute<Primary>(new string[] { "-h" });
            // CommandLineApplication.Execute<Primary>(new string[] { "-v", "-i:10", "-e:7" });
            // CommandLineApplication.Execute<Primary>(new string[] { "-v" });

            return 1;
        }
    }
}
