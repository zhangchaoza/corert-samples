namespace CommandLineUtils.Abstracttions
{
    using McMaster.Extensions.CommandLineUtils;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class BaseMainCommandLineApp : BaseApplcation
    {
        private CommandLineApplication _app;
        private readonly ConcurrentDictionary<string, BaseApplcation> _subApps;

        public override string Name => _app.Name;
        public override string FullName => _app.FullName;
        public override string Description => _app.Description;
        public override string ExtendedHelpText => _app.ExtendedHelpText;
        public override bool ThrowOnUnexpectedArg => _app.ThrowOnUnexpectedArgument;

        protected override CommandLineApplication App => _app;

        protected BaseMainCommandLineApp(
            string name,
            string fullName,
            string description,
            string extendedHelpText = null,
            bool throwOnUnexpectedArg = true)
        {
            _subApps = new ConcurrentDictionary<string, BaseApplcation>();
            _app = new CommandLineApplication(throwOnUnexpectedArg: throwOnUnexpectedArg)
            {
                Name = name,
                FullName = fullName,
                Description = description,
                ExtendedHelpText = extendedHelpText
            };
            _app.OnExecute(new Func<int>(OnExecute));
        }

        protected BaseMainCommandLineApp RegisterSubAppsCore(BaseSubCommandLineApp subapp)
        {
            _subApps.GetOrAdd(subapp.Name, key =>
            {
                _app.Command(
                    name: key,
                    configuration: subcla =>
                    {
                        subcla.FullName = subapp.FullName;
                        subcla.Description = subapp.Description;
                        subcla.ExtendedHelpText = subapp.ExtendedHelpText;
                        subapp
                            .SetApp(subcla)
                            .RegisterArguments()
                            .RegisterSubApps()
                            .RegisterOptions();

                        if (subapp is BaseAsyncSubCommandLineApp)
                        {
                            subcla.OnExecute(new Func<Task<int>>(() =>
                            {
                                subapp.OnBeforeExecute();
                                return (subapp as BaseAsyncSubCommandLineApp).OnExecuteAsync();
                            }));
                        }
                        else
                        {
                            subcla.OnExecute(new Func<int>(() =>
                            {
                                subapp.OnBeforeExecute();
                                return subapp.OnExecute();
                            }));
                        }
                    },
                    throwOnUnexpectedArg: subapp.ThrowOnUnexpectedArg);
                return subapp;
            });
            return this;
        }

        public int Execute(string[] args)
        {
            return this
                .RegisterArguments()
                .RegisterOptions()
                .RegisterSubApps()
                .AppExecute(args);
        }

        protected override int AppExecute(string[] args)
        {
            return _app.Execute(args);
        }

        protected virtual BaseMainCommandLineApp RegisterArguments()
        {
            return this;
        }

        protected virtual BaseMainCommandLineApp RegisterSubApps()
        {
            return this;
        }

        protected virtual BaseMainCommandLineApp RegisterOptions()
        {
            return this;
        }

        protected abstract int OnExecute();

    }
}