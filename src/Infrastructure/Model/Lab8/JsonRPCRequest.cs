using Infrastructure.Enum;
using System.Collections.Generic;

namespace Lab8_JsonRpc.Models
{
    public class JsonRPCRequest : JsonRPCBase
    {
        public JsonRPCMethod Method { get; set; }
        public Dictionary<string,object> Params { get; set; }
    }
}