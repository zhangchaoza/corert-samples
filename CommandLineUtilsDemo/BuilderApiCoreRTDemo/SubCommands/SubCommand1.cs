namespace BuilderApiCoreRTDemo.SubCommands
{
    using System;
    using System.Threading.Tasks;
    using CommandLineUtils.Abstracttions;
    using McMaster.Extensions.CommandLineUtils;

    public class SubCommand1 : BaseAsyncSubCommandLineApp
    {
        #region Arguments

        private CommandArgument<int> countArg;
        private CommandArgument<string> arg2;

        #endregion Arguments

        #region Options

        private CommandOption subject;
        private CommandOption<int> repeatOption;

        #endregion Options

        public override string Name => nameof(SubCommand1).ToLower();

        public override string FullName => $"{Name}的全名";

        public override string Description => "测试命令1";

        public override string ExtendedHelpText => null;

        protected override BaseSubCommandLineApp RegisterArguments()
        {
            countArg = App.Argument<int>(name: "count", description: "个数", configuration: countArg => { }, multipleValues: false);
            arg2 = App.Argument<string>(name: "arg2", description: "arg2", configuration: countArg => { }, multipleValues: false);
            return base.RegisterArguments();
        }

        protected override BaseSubCommandLineApp RegisterSubApps()
        {
            App.Command(name: "subcommand2", configuration: subapp => { });
            RegisterSubAppsCore(new DelayCommand());
            return base.RegisterSubApps();
        }

        protected override BaseSubCommandLineApp RegisterOptions()
        {
            subject = App.Option(template: "-s|--subject <SUBJECT>", description: "The subject", optionType: CommandOptionType.SingleValue, inherited: false);
            repeatOption = App.Option<int>(template: "-r|--repeat <N>", description: "Repeat", optionType: CommandOptionType.SingleValue, inherited: true, configuration: op =>
            {
                op.IsRequired(allowEmptyStrings: false, errorMessage: "Repeat不能为空");
            });
            return base.RegisterOptions();
        }

        protected override void OnBeforeExecute()
        {
            var v = App.GetOptions();
            var a = GetOption("v");
        }

        protected override Task<int> OnExecuteAsync()
        {
            var a = GetOption("v");
            Console.WriteLine(a.HasValue());
            var p = App.Parent;
            Console.WriteLine("Parent {0} {1}", p.Name, p.FullName);
            Console.WriteLine("Current {0} {1}", App.Name, App.FullName);
            foreach (var item in App.Options)
            {
                Console.WriteLine("{0} {1} {2} {3}", item.Description, item.SymbolName, item.ValueName, item.Value());
            }
            Console.WriteLine(countArg.ParsedValue);
            Console.WriteLine(arg2.ParsedValue);
            Console.WriteLine(subject.Value());
            if (repeatOption.HasValue())
            {
                Console.WriteLine("repeat:{0}", repeatOption.Value());
            }
            Console.WriteLine("fin");
            return Task.FromResult(0);
        }
    }
}
