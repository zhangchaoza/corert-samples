namespace CommandlineApiDemo
{
    using System.Collections.Generic;
    using System.CommandLine;

    internal class SimpleSuggestSource : ISuggestionSource
    {
        public IEnumerable<string> Suggest(string textToMatch = null)
        {
            return new string[]
            {
                "hello"
            };
        }
    }
}
