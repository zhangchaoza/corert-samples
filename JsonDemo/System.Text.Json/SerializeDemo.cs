namespace InternalJson
{
    using System;
    using System.Text.Json;

    public class SerializeDemo
    {
        public class WeatherForecast
        {
            public DateTimeOffset Date { get; set; }
            public int TemperatureCelsius { get; set; }
            public string Summary { get; set; }
        }

        public static void Serialize()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            string jsonString = JsonSerializer.Serialize(new WeatherForecast
            {
                Date = DateTimeOffset.UtcNow,
                TemperatureCelsius = 30,
                Summary = "济南Hot"
            }, options);
            Console.WriteLine(jsonString);
        }
    }
}
