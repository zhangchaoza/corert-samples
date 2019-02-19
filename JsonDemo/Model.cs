namespace JsonDemo
{
    using System.Collections.Generic;
    using System.Net;
    using Newtonsoft.Json;

    [JsonObject]
    public class Model
    {

        [JsonConstructor]
        public Model()
        {
        }

        [JsonProperty(propertyName: "Ip")]
        public IPEndPoint Ip { get; set; }

        public int Id { get; set; }

        public List<object> Strings { get; set; }

        public List<InnerObject> InnerArray { get; set; }

        public List<InnerObject> InnerList { get; set; }

        public IList<InnerObject> InnerIList { get; set; }
    }

    public class InnerObject
    {
        public string Name { get; set; }
    }
}