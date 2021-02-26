namespace InternalJson
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Serialize");
            SerializeDemo.Serialize();

            System.Console.WriteLine();

            System.Console.WriteLine("Deserialize");
            SerializeDemo.Deserialize();
        }

    }
}
