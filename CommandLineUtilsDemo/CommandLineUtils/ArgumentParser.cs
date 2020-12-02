namespace CommandLineUtils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ArgumentParser
    {
        public static string[] Parse(string cmdString)
        {
            var cmdRaw = cmdString.Trim().AsMemory();
            var indexs = new List<(int start, int end)>();

            int startIndex = 0;
            bool inBrackets = false;
            for (int i = 0; i < cmdRaw.Length; i++)
            {
                if (cmdRaw.Span[i].Equals('\'') || cmdRaw.Span[i].Equals('\"'))
                {
                    inBrackets = !inBrackets;
                }
                if (!inBrackets && cmdRaw.Span[i].Equals(' '))
                {
                    if (i != startIndex)
                    {
                        indexs.Add((startIndex, i));
                    }
                    startIndex = i + 1;
                }
            }
            indexs.Add((startIndex, cmdRaw.Length));

            return indexs
                .Select(i => cmdRaw
                    .Slice(i.start, i.end - i.start)
                    .ToString().Trim('"', '\''))
                .ToArray();
        }
    }
}
