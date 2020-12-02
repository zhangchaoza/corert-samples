namespace CommandLineUtils.Abstracttions
{
    using System.Threading.Tasks;

    public abstract class BaseAsyncSubCommandLineApp : BaseSubCommandLineApp
    {
        protected Task<int> AppExecuteAsnyc(string[] args)
        {
            return Task.FromResult(AppExecute(args));
        }

        protected internal override sealed int OnExecute()
        {
            return OnExecuteAsync().Result;
        }

        protected internal abstract Task<int> OnExecuteAsync();
    }
}
