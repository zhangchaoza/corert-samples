// Configure MQTT server.
using System.Net;
using MQTTnet;
using MQTTnet.Server;

var serverOptionsBuilder = new MqttServerOptionsBuilder()
    .WithoutDefaultEndpoint()
    .WithDefaultEndpoint()
    .WithDefaultEndpointBoundIPAddress(IPAddress.Any)
    .WithDefaultEndpointPort(1883);
var serverOptions = serverOptionsBuilder.Build();

var mqttFactory = new MqttFactory();
var mqttServer = mqttFactory.CreateMqttServer(serverOptions);
mqttServer.InterceptingPublishAsync += e =>
{
    Console.WriteLine("{0} {1}", e.ClientId, e.ApplicationMessage.Topic);
    return Task.CompletedTask;
};
await mqttServer.StartAsync();

var taskCompletion = new System.Threading.Tasks.TaskCompletionSource<int>();
Console.CancelKeyPress += (s, e) => taskCompletion.SetResult(0);
await taskCompletion.Task;
await mqttServer.StopAsync();
await Task.Delay(2000);
