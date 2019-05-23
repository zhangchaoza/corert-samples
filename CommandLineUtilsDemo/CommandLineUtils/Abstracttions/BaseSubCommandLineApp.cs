namespace CommandLineUtils.Abstracttions
{
    using McMaster.Extensions.CommandLineUtils;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class BaseSubCommandLineApp : BaseApplcation
    {

        private CommandLineApplication _app;

        protected override CommandLineApplication App => _app;

        internal BaseSubCommandLineApp SetApp(CommandLineApplication app)
        {
            _app = app;
            return this;
        }

        internal protected virtual BaseSubCommandLineApp RegisterArguments()
        {
            return this;
        }

        internal protected virtual BaseSubCommandLineApp RegisterSubApps()
        {
            return this;
        }

        internal protected virtual BaseSubCommandLineApp RegisterOptions()
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
        }

        protected override int AppExecute(string[] args)
        {
            return _app.Execute(args);
        }

        internal protected virtual void OnBeforeExecute()
        {
        }

        internal protected abstract int OnExecute();

    }
}