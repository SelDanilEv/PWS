using System.Collections.Generic;

namespace Lab8_JsonRpc.Models
{
    public class JsonRPCResponse : JsonRPCBase
    {
        public Dictionary<string,string> Result { get; set; }

        public static JsonRPCResponse Empty => new JsonRPCResponse();
    }
}