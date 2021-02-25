namespace JsonDemo
{
    using System;
    using System.Net;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class IPEndPointJsonConverter : JsonConverter
    {
        private readonly IPAddress _defaultIp;
        private readonly int _defaultPort;

        public IPEndPointJsonConverter(IPAddress defaultIp = null, int defaultPort = 20000)
        {
            _defaultIp = defaultIp ?? IPAddress.Loopback;
            _defaultPort = defaultPort;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IPEndPoint);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var ipspan = JToken.Load(reader).Value<string>().AsSpan();
            var splitindex = ipspan.IndexOf(':');

            IPAddress ip = _defaultIp;
            if (splitindex < 0)
            {
                ip = IPAddress.Parse(ipspan);
            }
            else if (splitindex == 0)
            {
                ip = _defaultIp;
            }
            else
            {
                ip = IPAddress.Parse(ipspan.Slice(0, splitindex));
            }

            int port = _defaultPort;
            if (splitindex >= 0)
            {
                port = int.Parse(ipspan.Slice(splitindex + 1));
            }

            return new IPEndPoint(ip, port);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
