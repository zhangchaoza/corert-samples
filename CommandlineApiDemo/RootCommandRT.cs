// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;

namespace CommandlineApiDemo
{
    /// <summary>
    /// 原RootCommand包含corert不支持的代码
    /// Assembly.Location在corert中为Empty
    /// </summary>
    public class RootCommandRT : Command
    {
        public RootCommandRT(
            string description = "",
            IReadOnlyCollection<Symbol> symbols = null,
            Argument argument = null,
            bool treatUnmatchedTokensAsErrors = true,
            ICommandHandler handler = null,
            bool isHidden = false) :
            base(ExeName, description)
        {
        }

        private static readonly Lazy<string> executableName =
            new Lazy<string>(() => Path.GetFileNameWithoutExtension(Environment.GetCommandLineArgs()[0]));

        public static string ExeName { get; } = executableName.Value;
    }
}
