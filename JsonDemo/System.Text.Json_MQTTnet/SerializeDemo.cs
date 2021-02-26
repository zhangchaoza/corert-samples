namespace InternalJson
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class SerializeDemo
    {

        public static void Serialize()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            // IList<MqttApplicationMessage>

            {
                var msg = new MQTTnet.MqttApplicationMessage
                {
                    Topic = "/test",
                    Payload = BitConverter.GetBytes(10),
                    QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce,
                    Retain = true,
                    UserProperties = new List<MQTTnet.Packets.MqttUserProperty> { new MQTTnet.Packets.MqttUserProperty("p1", "v1") },
                    ContentType = "type",
                    ResponseTopic = "/test2",
                    PayloadFormatIndicator = MQTTnet.Protocol.MqttPayloadFormatIndicator.CharacterData,
                    MessageExpiryInterval = 10,
                    TopicAlias = 2,
                    CorrelationData = BitConverter.GetBytes(11),
                    SubscriptionIdentifiers = new List<uint> { 1, 2, 3 }
                };
                string jsonString = JsonSerializer.Serialize(msg);
                Console.WriteLine(jsonString);
                Console.WriteLine();
            }

            {
                List<MQTTnet.MqttApplicationMessage> msg = new List<MQTTnet.MqttApplicationMessage>
                {
                    new MQTTnet.MqttApplicationMessage
                    {
                        Topic = "/test",
                        Payload = BitConverter.GetBytes(10),
                        QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce,
                        Retain = true,
                        UserProperties = new List<MQTTnet.Packets.MqttUserProperty> { new MQTTnet.Packets.MqttUserProperty("p1", "v1") },
                        ContentType = "type",
                        ResponseTopic = "/test2",
                        PayloadFormatIndicator = MQTTnet.Protocol.MqttPayloadFormatIndicator.CharacterData,
                        MessageExpiryInterval = 10,
                        TopicAlias = 2,
                        CorrelationData = BitConverter.GetBytes(11),
                        SubscriptionIdentifiers = new List<uint> { 1, 2, 3 }
                    },new MQTTnet.MqttApplicationMessage
                    {
                        Topic = "/test",
                        Payload = BitConverter.GetBytes(10),
                        QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce,
                        Retain = true,
                        UserProperties = new List<MQTTnet.Packets.MqttUserProperty> { new MQTTnet.Packets.MqttUserProperty("p1", "v1") },
                        ContentType = "type",
                        ResponseTopic = "/test2",
                        PayloadFormatIndicator = MQTTnet.Protocol.MqttPayloadFormatIndicator.CharacterData,
                        MessageExpiryInterval = 10,
                        TopicAlias = 2,
                        CorrelationData = BitConverter.GetBytes(11),
                        SubscriptionIdentifiers = new List<uint> { 1, 2, 3 }
                    }
                };
                string jsonString = JsonSerializer.Serialize(msg);
                Console.WriteLine(jsonString);
                Console.WriteLine();
            }

            {
                IList<MQTTnet.MqttApplicationMessage> msg = new List<MQTTnet.MqttApplicationMessage>
                {
                    new MQTTnet.MqttApplicationMessage
                    {
                        Topic = "/test",
                        Payload = BitConverter.GetBytes(10),
                        QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce,
                        Retain = true,
                        UserProperties = new List<MQTTnet.Packets.MqttUserProperty> { new MQTTnet.Packets.MqttUserProperty("p1", "v1") },
                        ContentType = "type",
                        ResponseTopic = "/test2",
                        PayloadFormatIndicator = MQTTnet.Protocol.MqttPayloadFormatIndicator.CharacterData,
                        MessageExpiryInterval = 10,
                        TopicAlias = 2,
                        CorrelationData = BitConverter.GetBytes(11),
                        SubscriptionIdentifiers = new List<uint> { 1, 2, 3 }
                    },new MQTTnet.MqttApplicationMessage
                    {
                        Topic = "/test",
                        Payload = BitConverter.GetBytes(10),
                        QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce,
                        Retain = true,
                        UserProperties = new List<MQTTnet.Packets.MqttUserProperty> { new MQTTnet.Packets.MqttUserProperty("p1", "v1") },
                        ContentType = "type",
                        ResponseTopic = "/test2",
                        PayloadFormatIndicator = MQTTnet.Protocol.MqttPayloadFormatIndicator.CharacterData,
                        MessageExpiryInterval = 10,
                        TopicAlias = 2,
                        CorrelationData = BitConverter.GetBytes(11),
                        SubscriptionIdentifiers = new List<uint> { 1, 2, 3 }
                    }
                };
                string jsonString = JsonSerializer.Serialize(msg);
                Console.WriteLine(jsonString);
                Console.WriteLine();
            }
        }

        public static void Deserialize()
        {

            var options = new JsonSerializerOptions
            {
                WriteIndented = false,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };

            {
                string json = @"{""Topic"":""/test"",""Payload"":""CgAAAA=="",""QualityOfServiceLevel"":1,""Retain"":true,""UserProperties"":[{""Name"":""p1"",""Value"":""v1""}],""ContentType"":""type"",""ResponseTopic"":""/test2"",""PayloadFormatIndicator"":1,""MessageExpiryInterval"":10,""TopicAlias"":2,""CorrelationData"":""CwAAAA=="",""SubscriptionIdentifiers"":[1,2,3]}";
                {
                    var obj = JsonSerializer.Deserialize<MQTTnet.MqttApplicationMessage>(json, options);
                    var json2 = JsonSerializer.Serialize(obj, options);
                    Console.WriteLine(json2);
                }

                {
                    var obj = JsonSerializer.Deserialize(json, typeof(MQTTnet.MqttApplicationMessage), options);
                    var json2 = JsonSerializer.Serialize(obj, options);
                    Console.WriteLine(json2);
                }
                Console.WriteLine();
            }

            {
                string json = @"[{""Topic"":""/test"",""Payload"":""CgAAAA=="",""QualityOfServiceLevel"":1,""Retain"":true,""UserProperties"":[{""Name"":""p1"",""Value"":""v1""}],""ContentType"":""type"",""ResponseTopic"":""/test2"",""PayloadFormatIndicator"":1,""MessageExpiryInterval"":10,""TopicAlias"":2,""CorrelationData"":""CwAAAA=="",""SubscriptionIdentifiers"":[1,2,3]},{""Topic"":""/test"",""Payload"":""CgAAAA=="",""QualityOfServiceLevel"":1,""Retain"":true,""UserProperties"":[{""Name"":""p1"",""Value"":""v1""}],""ContentType"":""type"",""ResponseTopic"":""/test2"",""PayloadFormatIndicator"":1,""MessageExpiryInterval"":10,""TopicAlias"":2,""CorrelationData"":""CwAAAA=="",""SubscriptionIdentifiers"":[1,2,3]}]";

                var obj = JsonSerializer.Deserialize<List<MQTTnet.MqttApplicationMessage>>(json, options);
                var json2 = JsonSerializer.Serialize(obj, options);
                Console.WriteLine(json2);
                Console.WriteLine();
            }
        }
    }
}
