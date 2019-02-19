using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = @"
{
    'Ip':'10.0.1.2:8000',
    'Id':12,
    'Strings':['1','2'],
    'InnerArray':[
        {
            'Name':'obj1'
        },
        {
            'Name':'obj2'
        }
    ],
    'InnerList':[
        {
            'Name':'obj3'
        },
        {
            'Name':'obj4'
        }
    ],
    'InnerIList':[
        {
            'Name':'obj3'
        },
        {
            'Name':'obj4'
        }
    ]
}
            ";

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new IPEndPointJsonConverter());

            NoReflectionTest(json);

            ReflectionTest(json, settings);
            Console.ReadLine();
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
