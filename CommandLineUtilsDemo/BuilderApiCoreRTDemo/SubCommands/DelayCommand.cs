namespace BuilderApiCoreRTDemo.SubCommands
{
    using System;
    using System.Threading.Tasks;
    using CommandLineUtils.Abstracttions;

    public class DelayCommand : BaseAsyncSubCommandLineApp
    {
        public override string Name => nameof(DelayCommand).ToLower();

        public override string FullName => $"{Name}的全名";

        public override string Description => "长时间测试";

        public override string ExtendedHelpText => null;

        protected override Task<int> OnExecuteAsync()
        {
            var s = GetOption("subject");
            var r = GetOption("repeat");
            Console.WriteLine((s?.HasValue() ?? false) ? s.Value() : "null");
            Console.WriteLine((r?.HasValue() ?? false) ? r.Value() : "null");

            Console.WriteLine("delay start");
            return Task.Delay(10000)
                .ContinueWith(t => 0);
        }
    }
}
