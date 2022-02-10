namespace InternalJson
{
    class Program
    {
        static void Main(string[] args)
        {
            SerializeDemo.Serialize();
            SerializeDemo.Deserialize();
            SerializeDemo.DeserializeJsonDocument();
            SerializeDemo.SerializeUtf8JsonWriter();
            SerializeDemo.DeserializeUtf8JsonWriter();
            SerializeDemo.UseAnonymous();
        }

    }
}
