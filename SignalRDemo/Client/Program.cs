using System;
using System.Collections.Generic;
using Common;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:8080/consolehub")
                .Build();

            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Console.WriteLine($"server: {user}: {message}");
            });

            try
            {
                connection.StartAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("SimpleParamTest:");
            SimpleParamTest(connection);

            // Console.WriteLine("ObjParamTest:");
            // ObjParamTest(connection);

            // Console.WriteLine("ObjParamWithArrayTest:");
            // ObjParamWithArrayTest(connection);

            // Console.WriteLine("ObjParamWithListTest:");
            // ObjParamWithListTest(connection);

            // Console.WriteLine("ObjParamWithResultTest:");
            // ObjParamWithResultTest(connection);

            // Console.WriteLine("ObjParamWithArrayWithResultTest:");
            // ObjParamWithArrayWithResultTest(connection);

            // Console.WriteLine("ObjParamWithListWithResultTest:");
            // ObjParamWithListWithResultTest(connection);

            try
            {
                connection.StopAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }

        private static void SimpleParamTest(HubConnection connection)
        {
            connection.InvokeAsync("SendMessage", "user1", "hello.").GetAwaiter().GetResult();
        }

        private static void ObjParamTest(HubConnection connection)
        {
            var param = new ObjParam()
            {
                Id = 1,
                Name = "zhangchao",
                Message = new Message
                {
                    Id = 1,
                    Name = "hello",
                }
            };
            // Console.WriteLine(JsonConvert.SerializeObject(param1));
            Console.WriteLine(connection.InvokeAsync<string>("SendObject", param).Result);
        }

        private static void ObjParamWithResultTest(HubConnection connection)
        {
            var param = new ObjParam()
            {
                Id = 1,
                Name = "zhangchao",
                Message = new Message
                {
                    Id = 1,
                    Name = "hello",
                }
            };
            // Console.WriteLine(JsonConvert.SerializeObject(param1));
            Console.WriteLine(JsonConvert.SerializeObject(connection.InvokeAsync<ObjParam>("SendObjectWithResult", param).Result));
        }

        private static void ObjParamWithArrayTest(HubConnection connection)
        {
            var param = new ObjParamWithArray
            {
                Id = 1,
                Name = "zhangchao",
                Messages = new Message[]
                            {
                    new Message
                    {
                        Id = 1,
                        Name = "hello"
                    },
                    new Message
                    {
                        Id = 2,
                        Name = "world"
                    },
                            }
            };
            // Console.WriteLine(JsonConvert.SerializeObject(param2));
            Console.WriteLine(connection.InvokeAsync<string>("SendObjectWithArray", param).Result);
        }

        private static void ObjParamWithArrayWithResultTest(HubConnection connection)
        {
            var param = new ObjParamWithArray
            {
                Id = 1,
                Name = "zhangchao",
                Messages = new Message[]
                            {
                    new Message
                    {
                        Id = 1,
                        Name = "hello"
                    },
                    new Message
                    {
                        Id = 2,
                        Name = "world"
                    },
                            }
            };
            // Console.WriteLine(JsonConvert.SerializeObject(param2));
            Console.WriteLine(JsonConvert.SerializeObject(connection.InvokeAsync<ObjParamWithArray>("SendObjectWithArrayWithResult", param).Result));
        }

        private static void ObjParamWithListTest(HubConnection connection)
        {
            var param = new ObjParamWithList
            {
                Id = 1,
                Name = "zhangchao",
                Messages = new List<Message>
                {
                    new Message
                    {
                        Id = 1,
                        Name = "hello"
                    },
                    new Message
                    {
                        Id = 2,
                        Name = "world"
                    },
                }
            };
            // Console.WriteLine(JsonConvert.SerializeObject(param2));
            Console.WriteLine(connection.InvokeAsync<string>("SendObjectWithList", param).Result);
        }

        private static void ObjParamWithListWithResultTest(HubConnection connection)
        {
            var param = new ObjParamWithList
            {
                Id = 1,
                Name = "zhangchao",
                Messages = new List<Message>
                {
                    new Message
                    {
                        Id = 1,
                        Name = "hello"
                    },
                    new Message
                    {
                        Id = 2,
                        Name = "world"
                    },
                }
            };
            // Console.WriteLine(JsonConvert.SerializeObject(param2));
            Console.WriteLine(JsonConvert.SerializeObject(connection.InvokeAsync<ObjParamWithList>("SendObjectWithListWithResult", param).Result));
        }
    }
}
