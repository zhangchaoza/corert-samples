﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;

namespace NativeLibrary
{
    public class Class1
    {
        [NativeCallable(EntryPoint = "add", CallingConvention = CallingConvention.StdCall)]
        public static int Add(int a, int b)
        {
            return a + b;
        }

        [NativeCallable(EntryPoint = "write_line", CallingConvention = CallingConvention.StdCall)]
        public static int WriteLine(IntPtr pString)
        {
            // The marshalling code is typically auto-generated by a custom tool in larger projects.
            try
            {
                // NativeCallable methods only accept primitive arguments. The primitive arguments
                // have to be marshalled manually if necessary.
                string str = Marshal.PtrToStringAnsi(pString);
                Console.WriteLine(str);
            }
            catch
            {
                // Exceptions escaping out of NativeCallable methods are treated as unhandled exceptions.
                // The errors have to be marshalled manually if necessary.
                return -1;
            }
            return 0;
        }
    }
}
