using MQTTnet;
using MQTTnet.Client;

var optionBuilder = new MqttClientOptionsBuilder()
    .WithClientId($"{Guid.NewGuid()}")
    .WithCredentials("mySecretUser", "mySecretPassword")
    .WithTcpServer("127.0.0.1", 1883)
    .WithCleanSession();
var options = optionBuilder.Build();

var factory = new MqttFactory();
var mqttClient = factory.CreateMqttClient();
mqttClient.ApplicationMessageReceivedAsync += e =>
{
    Console.WriteLine("{0} {1} {2}", e.ClientId, e.ApplicationMessage.Topic, e.ApplicationMessage.Payload.Length);
    return Task.CompletedTask;
};

await mqttClient.ConnectAsync(options, CancellationToken.None);
await mqttClient.SubscribeAsync(new MqttClientSubscribeOptionsBuilder()
    .WithTopicFilter(new MqttTopicFilterBuilder()
        .WithTopic("my/topic"))
    .Build(), CancellationToken.None);
await mqttClient.PublishAsync(new MqttApplicationMessageBuilder()
    .WithTopic("my/topic")
    .WithPayload("hello world")
    .Build(), CancellationToken.None);

await Task.Delay(5000);
await mqttClient.DisconnectAsync(new MqttClientDisconnectOptions(), CancellationToken.None);
