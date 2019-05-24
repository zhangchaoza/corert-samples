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

            Console.WriteLine("===================");

            {
                {
                    var fixobj = new Example2();
                    fixobj.Generic(1);
                    fixobj.Generic("");
                    fixobj.Generic(true);
                }

                var obj = new Example2();
                var s_GetParserGeneric = typeof(Example2).GetTypeInfo()
                    .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Single(m => m.Name == nameof(obj.Generic) && m.IsGenericMethod);

                foreach (var item in new object[] { 1, "1_str", false })
                {
                    Console.WriteLine($"test type:{item.GetType()}");
                    var method = s_GetParserGeneric.MakeGenericMethod(item.GetType());
                    method.Invoke(obj, new object[] { item });
                }
            }

            // {
            //     var obj = new Example2();
            //     var s_GetParserGeneric = typeof(Example2).GetTypeInfo()
            //         .GetMethods(BindingFlags.Instance | BindingFlags.Public)
            //         .Single(m => m.Name == "Generic2" && !m.IsGenericMethod);
            //     //var method = s_GetParserGeneric.MakeGenericMethod(typeof(int));
            //     // Console.WriteLine(method);
            //     s_GetParserGeneric.Invoke(obj, new object[] { typeof(int), 1 });
            // }
        }

    }

    public static class Example
    {
        public static void Generic<T>(T toDisplay)
        {
            Console.WriteLine("\r\nHere it is: {0}", toDisplay);
        }

    }

    /// <summary>
    /// rd.xml不支持重载方法
    /// </summary>
    public class Example2
    {
        public void Generic(Type Type, object toDisplay)
        {
            Console.WriteLine("(1)Here it is:{0},{1}", Type, toDisplay);
        }

        public void Generic<T>(T toDisplay)
        {
            Console.WriteLine("(2)Here it is:{0},{1}", typeof(T), toDisplay);
        }

    }

}
