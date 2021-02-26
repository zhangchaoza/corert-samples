namespace InternalJson
{
    using System;
    using System.Buffers;
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

        public static void DeserializeJsonDocument()
        {
            var json = @"
{
  ""Date"": ""2021-02-26T03:30:17.2534607+00:00"",
  ""TemperatureCelsius"": 30,
  ""Summary"": ""济南Hot"",
  ""Strings"": [
    ""abc"",
    123
  ],
  ""Objects"": [
    ""abc"",
    123,
    {
      ""Name"": ""abc""
    }
  ],
  ""InnerArray"": [
    {
      ""Name"": ""abc""
    }
  ],
  ""InnerList"": [
    {
      ""Name"": ""abc""
    }
  ],
  ""InnerIList"": [
    {
      ""Name"": ""abc""
    }
  ]
}
            ";

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            var jDoc = JsonSerializer.Deserialize<JsonDocument>(json, options);
            if (jDoc.RootElement.TryGetProperty("Date", out var p))
            {
                Console.WriteLine(p.GetDateTime());
            }
        }

        public static void SerializeUtf8JsonWriter()
        {
            var arrayBufferWriter = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(arrayBufferWriter, new JsonWriterOptions
            {
                // Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(allowedRanges: UnicodeRanges.All),
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                Indented = true
            }))
            {
                writer.WriteStartObject();

                writer.WriteNumber("number", 15);
                writer.WriteString("english", "Smith");
                writer.WriteString("汉字", "你");
                writer.WriteString("symbol", @"~`!@#$%^&*()_-+={}[]:;'<>,.?/ ");
                writer.WriteString("chinese_symbol", @"~·@#￥%……&*（）—-+=｛｝【】；：“”‘’《》，。？、");

                writer.WriteStartArray("phoneNumbers");
                writer.WriteStringValue("425-000-1212");
                writer.WriteStringValue("425-000-1213");
                writer.WriteEndArray();

                writer.WriteStartObject("address");
                writer.WriteString("street", "1 Microsoft Way");
                writer.WriteString("city", "Redmond");
                writer.WriteNumber("zip", 98052);
                writer.WriteEndObject();

                writer.WriteEndObject();
                writer.Flush();
            }
            Console.WriteLine(System.Text.UTF8Encoding.UTF8.GetString(arrayBufferWriter.WrittenSpan));
        }

        public static void DeserializeUtf8JsonWriter()
        {
            var json = @"
{
  ""number"": 15,
  ""english"": ""Smith"",
  ""汉字"": ""你"",// Comment
  ""symbol"": ""~`!@#$%^&*()_-+={}[]:;'<>,.?/ "",
  ""chinese_symbol"": ""~·@#￥%……&*（）—-+=｛｝【】；：“”‘’《》，。？、"",
  ""phoneNumbers"": [
    ""425-000-1212"",
    ""425-000-1213""
  ],
  ""address"": {
    ""street"": ""1 Microsoft Way"",
    ""city"": ""Redmond"",
    ""zip"": 98052,
    ""info"": null
  }
}
            ";

            var readerOption = new JsonReaderOptions
            {
                AllowTrailingCommas = true,
                CommentHandling = JsonCommentHandling.Allow
            };

            int deep = 0;
            var jsonReader = new Utf8JsonReader(System.Text.UTF8Encoding.UTF8.GetBytes(json).AsSpan(), readerOption);
            while (jsonReader.Read())
            {
                var o = jsonReader.TokenType switch
                {
                    JsonTokenType.StartObject => $"{Environment.NewLine}{GetPlaceHolder(deep++)}StartObject",
                    JsonTokenType.EndObject => $"{Environment.NewLine}{GetPlaceHolder(--deep)}EndObject",
                    JsonTokenType.StartArray => $"{Environment.NewLine}{GetPlaceHolder(deep++)}StartArray",
                    JsonTokenType.EndArray => $"{Environment.NewLine}{GetPlaceHolder(--deep)}EndArray",
                    JsonTokenType.PropertyName => $"{Environment.NewLine}{GetPlaceHolder(deep)}Property:{jsonReader.GetString()}",
                    JsonTokenType.Comment => $" Comment:{jsonReader.GetComment()}",
                    JsonTokenType.String => $" String:{jsonReader.GetString()}",
                    JsonTokenType.Number => $" Number:{jsonReader.GetDouble()}",
                    JsonTokenType.True => $" True:{jsonReader.GetBoolean()}",
                    JsonTokenType.False => $" False:{jsonReader.GetBoolean()}",
                    JsonTokenType.Null => $" Null",
                    _ => "",
                };
                Console.Write(o);
            }

            static string GetPlaceHolder(int d) => new string('\t', d);
        }

    }
}
