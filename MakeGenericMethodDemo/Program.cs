using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MakeGenericMethodDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\r\n--- Examine a generic method.");

            Type ex = typeof(Example);
            MethodInfo mi = ex.GetMethod("Generic");

            MethodInfo miConstructed = mi.MakeGenericMethod(typeof(Int32));
            object[] oargs = { 42 };
            miConstructed.Invoke(null, oargs);

            MethodInfo miDef = miConstructed.GetGenericMethodDefinition();
            Console.WriteLine("\r\nThe definition is the same: {0}",
                miDef == mi);

            Console.WriteLine("dynamic test");
            dynamic d = (object)42;
            Example.Generic(d);

            Console.WriteLine("IList test");
            Type te = typeof(Enumerable);
            MethodInfo mta = te.GetMethod("ToArray");
            Console.WriteLine("GetMethod");
            MethodInfo mtaConstructed = mta.MakeGenericMethod(typeof(Int32));
            Console.WriteLine("MakeGenericMethod");
            var array = (int[])mtaConstructed.Invoke(null, new object[] { new int[] { 1, 2 } });
            Console.WriteLine($"Length:{array.Length}");

            Console.WriteLine("dynamic IList test");
            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(typeof(Int32)));
            list.Add(1);
            list.Add(2);
            array = (int[])Enumerable.ToArray((dynamic)list);
            Console.WriteLine($"Length:{array.Length}");
        }

    }

    public static class Example
    {
        public static void Generic<T>(T toDisplay)
        {
            Console.WriteLine("\r\nHere it is: {0}", toDisplay);
        }

    }
}
