using System.Collections.Generic;

namespace Common
{
    public class ObjParam
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Message Message { get; set; }
    }

    public class ObjParamWithArray
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Message[] Messages { get; set; }
    }

    public class ObjParamWithList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<Message> Messages { get; set; }
    }

    public class Message
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
