namespace CommandLineUtils.Abstracttions
{
    using System.Linq;
    using McMaster.Extensions.CommandLineUtils;

    public abstract class BaseApplcation
    {
        public abstract string Name { get; }
        public abstract string FullName { get; }
        public abstract string Description { get; }
        public abstract string ExtendedHelpText { get; }
        public abstract bool ThrowOnUnexpectedArg { get; }
        protected abstract CommandLineApplication App { get; }

        protected abstract int AppExecute(string[] args);

        protected CommandOption GetOption(string name)
        {
            return App.GetOptions().SingleOrDefault(i => i.ShortName == name || i.LongName == name);
        }
    }
}
