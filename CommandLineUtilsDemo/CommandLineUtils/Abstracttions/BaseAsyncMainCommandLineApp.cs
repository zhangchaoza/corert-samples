namespace CommandLineUtils.Abstracttions
{
    using System.Threading.Tasks;

    public abstract class BaseAsyncMainCommandLineApp : BaseMainCommandLineApp
    {
        protected BaseAsyncMainCommandLineApp(string name, string fullName, string description, string extendedHelpText = null, bool throwOnUnexpectedArg = true)
            : base(name, fullName, description, extendedHelpText, throwOnUnexpectedArg)
        {
        }

        protected Task<int> AppExecuteAsnyc(string[] args)
        {
            return Task.FromResult(AppExecute(args));
        }

        protected sealed override int OnExecute()
        {
            return OnExecuteAsync().Result;
        }

        protected abstract Task<int> OnExecuteAsync();
    }
}