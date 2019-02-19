namespace Server
{
    using Common;
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine($"{user}:{message}");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public Task<string> SendObject(ObjParam param)
        {
            string value = JsonConvert.SerializeObject(param);
            Console.WriteLine(value);
            return Task.FromResult(value);
        }

        public Task<ObjParam> SendObjectWithResult(ObjParam param)
        {
            string value = JsonConvert.SerializeObject(param);
            Console.WriteLine(value);
            return Task.FromResult(param);
        }

        public Task<string> SendObjectWithArray(ObjParamWithArray param)
        {
            string value = JsonConvert.SerializeObject(param);
            Console.WriteLine(value);
            return Task.FromResult(value);
        }

        public Task<ObjParamWithArray> SendObjectWithArrayWithResult(ObjParamWithArray param)
        {
            string value = JsonConvert.SerializeObject(param);
            Console.WriteLine(value);
            return Task.FromResult(param);
        }

        public Task<string> SendObjectWithList(ObjParamWithList param)
        {
            string value = JsonConvert.SerializeObject(param);
            Console.WriteLine(value);
            return Task.FromResult(value);
        }

        public Task<ObjParamWithList> SendObjectWithListWithResult(ObjParamWithList param)
        {
            string value = JsonConvert.SerializeObject(param);
            Console.WriteLine(value);
            return Task.FromResult(param);
        }
    }

}