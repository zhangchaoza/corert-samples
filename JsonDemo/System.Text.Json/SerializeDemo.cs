namespace InternalJson
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class SerializeDemo
    {
        public class WeatherForecast
        {
            [JsonConstructor]
            public WeatherForecast()
            {
            }

            [JsonPropertyName("Date")]
            public DateTimeOffset Date { get; set; }
            public int TemperatureCelsius { get; set; }
            public string Summary { get; set; }
            public List<object> Strings { get; set; }
            public List<object> Objects { get; set; }
            public InnerObject[] InnerArray { get; set; }
            public List<InnerObject> InnerList { get; set; }
            public IList<InnerObject> InnerIList { get; set; }
        }

        public class InnerObject
        {
            public string Name { get; set; }
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
                Summary = "济南Hot",
                Strings = new List<object> { "abc", 123, },
                Objects = new List<object> { "abc", 123, new InnerObject { Name = "abc" } },
                InnerArray = new InnerObject[] { new InnerObject { Name = "abc" } },
                InnerList = new List<InnerObject> { new InnerObject { Name = "abc" } },
                InnerIList = new List<InnerObject> { new InnerObject { Name = "abc" } },
            }, options);
            Console.WriteLine(jsonString);
        }

        public static void Deserialize()
        {
            string json = @"
{
    ""Date"":""2021-02-25T10:20:46.8651887+00:00"",
    ""TemperatureCelsius"":30,
    ""Summary"":""济南Hot"",
    ""Strings"":[""1"",""2""],
    ""Objects"":[""1"",2,{""Name"":""abc你""}],
    ""InnerArray"":[
        {
            ""Name"":""abc你""
        },
        {
            ""Name"":""abc你""
        }
    ],
    ""InnerList"":[
        {
            ""Name"":""abc你""
        },
        {
            ""Name"":""abc你""
        }
    ],
    ""InnerIList"":[
        {
            ""Name"":""abc你""
        },
        {
            ""Name"":""abc你""
        }
    ]
}
            ";

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            {
                var obj = JsonSerializer.Deserialize(json, typeof(WeatherForecast), options);
                var json2 = JsonSerializer.Serialize(obj, options);
                Console.WriteLine(json2);
            }

            {
                var obj = JsonSerializer.Deserialize<WeatherForecast>(json, options);
                var json2 = JsonSerializer.Serialize(obj, options);
                Console.WriteLine(json2);
            }
        }
    }
}
