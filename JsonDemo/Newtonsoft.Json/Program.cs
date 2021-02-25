using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Collections.Generic;

namespace JsonDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Serialize();
            Deserialize();
            Console.ReadLine();
        }

        private static void Serialize()
        {
            var m = new Model
            {
                Ip = new IPEndPoint(IPAddress.Loopback, 8080),
                Id = 10,
                Strings = new List<object> { "123", "abc你", 123 },
                Objects = new List<object> { "123", "abc你", 123, new InnerObject { Name = "abc你" } },
                InnerArray = new InnerObject[] { new InnerObject { Name = "abc你" } },
                InnerList = new List<InnerObject> { new InnerObject { Name = "abc你" } },
                InnerIList = new List<InnerObject> { new InnerObject { Name = "abc你" } },
            };

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new IPEndPointJsonConverter());
            var json = JsonConvert.SerializeObject(m, settings);
            Console.WriteLine(json);
        }

        private static void Deserialize()
        {
            string json = @"
{
    'Ip':'10.0.1.2:8000',
    'Id':12,
    'Strings':['1','2'],
    'Objects':['1',2,{'Name':'abc你'}],
    'InnerArray':[
        {
            'Name':'abc你'
        },
        {
            'Name':'abc你'
        }
    ],
    'InnerList':[
        {
            'Name':'abc你'
        },
        {
            'Name':'abc你'
        }
    ],
    'InnerIList':[
        {
            'Name':'abc你'
        },
        {
            'Name':'abc你'
        }
    ]
}
            ";

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new IPEndPointJsonConverter());

            NoReflectionTest(json);

            ReflectionTest(json, settings);
        }

        private static void NoReflectionTest(string json)
        {
            Console.WriteLine("NoReflectionTest");

            //不使用反射，直接用JObject就可以了
            var obj = JsonConvert.DeserializeObject<JObject>(json);

            if (null == obj)
            {
                Console.WriteLine("false");
            }
            else
            {
                Console.WriteLine("DeserializeObject finished.");
                Console.WriteLine(obj);
                Console.WriteLine(obj["Id"]);
                Console.WriteLine(obj["Strings"]);
                Console.WriteLine(((JArray)obj["Strings"]).Count);
                Console.WriteLine(JsonConvert.SerializeObject(obj));
            }
        }

        private static void ReflectionTest(string json, JsonSerializerSettings jsonSetting)
        {
            Console.WriteLine("ReflectionTest");

            var obj = JsonConvert.DeserializeObject<Model>(json, jsonSetting);
            Console.WriteLine(JsonConvert.SerializeObject(obj, jsonSetting));
        }
    }
}
