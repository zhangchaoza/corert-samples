namespace CommandlineApiDemo
{
    using System.Collections.Generic;
    using System.CommandLine.Completions;

    internal class SimpleSuggestSource : ICompletionSource
    {
        public IEnumerable<CompletionItem> GetCompletions(CompletionContext context)
        {
            return new CompletionItem[]
            {
                new CompletionItem("hello")
            };
        }

    }
}
