namespace CommandLineUtils.Abstracttions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class BaseAsyncSubCommandLineApp : BaseSubCommandLineApp
    {
        protected Task<int> AppExecuteAsnyc(string[] args)
        {
            return Task.FromResult(AppExecute(args));
        }

        internal protected sealed override int OnExecute()
        {
            return OnExecuteAsync().Result;
        }

        internal protected abstract Task<int> OnExecuteAsync();

    }
}