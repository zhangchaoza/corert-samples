using System.Reflection;
using System.Threading.Tasks;
using BuilderApiCoreRTDemo.SubCommands;
using CommandLineUtils.Abstracttions;
using McMaster.Extensions.CommandLineUtils;

namespace BuilderApiCoreRTDemo
{
    public class MainCommand : BaseAsyncMainCommandLineApp
    {
        public MainCommand(string name, string fullName, string description, string extendedHelpText = null, bool throwOnUnexpectedArg = true)
            : base(name, fullName, description, extendedHelpText, throwOnUnexpectedArg)
        {
        }

        protected override BaseMainCommandLineApp RegisterArguments()
        {
            return base.RegisterArguments();
        }

        protected override BaseMainCommandLineApp RegisterSubApps()
        {
            RegisterSubAppsCore(new SubCommand1());
            RegisterSubAppsCore(new DelayCommand());
            return base.RegisterSubApps();
        }

        protected override BaseMainCommandLineApp RegisterOptions()
        {
            var assembly = typeof(MainCommand).Assembly;
            var shortversion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            App.HelpOption(template: "-h|--help", inherited: true);
            App.VersionOption(
                template: "--version",
                shortFormVersion: shortversion,
                longFormVersion: $"{assembly.GetName().Name} {shortversion}");
            App.Option(template: "-v|--verbose", description: "显示更多信息", optionType: CommandOptionType.NoValue, inherited: true);
            App.Option(template: "-a", description: "int value", optionType: CommandOptionType.SingleValue, inherited: true);
            App.Option(template: "--bool", description: "bool value", optionType: CommandOptionType.SingleValue, inherited: true);
            return base.RegisterOptions();
        }

        protected override Task<int> OnExecuteAsync()
        {
            return AppExecuteAsnyc(new string[] { "-h" });
        }
    }
}
