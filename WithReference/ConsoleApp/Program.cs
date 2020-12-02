using System;
using Common;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var u = new User { Name = "zack" };
            Console.WriteLine(u.Name);
            Console.ReadLine();
        }
    }
}
