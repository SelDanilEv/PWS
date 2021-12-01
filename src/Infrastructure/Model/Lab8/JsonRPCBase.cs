using System.Collections.Generic;

namespace Lab8_JsonRpc.Models
{
    public class JsonRPCBase
    {
        public JsonRPCBase()
        {
        }

        public JsonRPCBase(string id, string jsonRPC)
        {
            Id = id;
            JsonRPC = jsonRPC;
        }

        public string Id { get; set; }
        public string JsonRPC { get; set; }
    }
}