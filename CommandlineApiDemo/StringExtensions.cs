// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandlineApiDemo
{
    public static class StringExtensions
    {
        private static readonly string[] _optionPrefixStrings = { "--", "-", "/" };

        private static readonly Regex _tokenizer = new Regex(
            @"((?<opt>[^""\s]+)""(?<arg>[^""]+)"") # token + quoted argument with non-space argument delimiter, ex: --opt:""c:\path with\spaces""
              |                                
              (""(?<token>[^""]*)"")               # tokens surrounded by spaces, ex: ""c:\path with\spaces""
              |
              (?<token>\S+)                        # tokens containing no quotes or spaces
              ",
            RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace
        );

        internal static string RemovePrefix(this string option)
        {
            foreach (var prefix in _optionPrefixStrings)
            {
                if (option.StartsWith(prefix))
                {
                    return option.Substring(prefix.Length);
                }
            }

            return option;
        }

    }
}
