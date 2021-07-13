namespace CommandlineApiDemo
{
    using System.Collections.Generic;
    using System.CommandLine.Parsing;
    using System.CommandLine.Suggestions;

    internal class SimpleSuggestSource : ISuggestionSource
    {
        public IEnumerable<string> GetSuggestions(ParseResult parseResult = null, string textToMatch = null)
        {
            return new string[]
            {
                "hello"
            };
        }
    }
}
