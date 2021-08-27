namespace BogusDemo
{

    using Bogus.DataSets;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Address().Latitude());
        }
    }
}
