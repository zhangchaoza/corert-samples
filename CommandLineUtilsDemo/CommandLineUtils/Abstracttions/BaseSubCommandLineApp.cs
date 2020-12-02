namespace CommandLineUtils.Abstracttions
{
    using System;
    using McMaster.Extensions.CommandLineUtils;

    public abstract class BaseSubCommandLineApp : BaseApplcation
    {
        private CommandLineApplication _app;

        protected override CommandLineApplication App => _app;

        internal BaseSubCommandLineApp SetApp(CommandLineApplication app)
        {
            _app = app;
            return this;
        }

        protected internal virtual BaseSubCommandLineApp RegisterArguments()
        {
            return this;
        }

        protected internal virtual BaseSubCommandLineApp RegisterSubApps()
        {
            return this;
        }

        protected internal virtual BaseSubCommandLineApp RegisterOptions()
        {
            return this;
        }

        protected BaseSubCommandLineApp RegisterSubAppsCore(BaseSubCommandLineApp subapp)
        {
            _app.Command(
                name: subapp.Name,
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
                        subcla.OnExecuteAsync(token =>
                        {
                            subapp.OnBeforeExecute();
                            return (subapp as BaseAsyncSubCommandLineApp).OnExecuteAsync();
                        });
                    }
                    else
                    {
                        subcla.OnExecute(new Func<int>(() =>
                        {
                            subapp.OnBeforeExecute();
                            return subapp.OnExecute();
                        }));
                    }
                });
            return subapp;
        }

        protected override int AppExecute(string[] args)
        {
            return _app.Execute(args);
        }

        protected internal virtual void OnBeforeExecute()
        {
        }

        protected internal abstract int OnExecute();
    }
}
