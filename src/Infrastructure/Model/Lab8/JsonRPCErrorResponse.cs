namespace Lab8_JsonRpc.Models
{
    public class JsonRPCErrorResponse : JsonRPCResponse
    {
        public JsonRPCError Error { get; set; }

        public static JsonRPCErrorResponse FromCommonResponse(JsonRPCResponse from, JsonRPCError error)
        {
            return new JsonRPCErrorResponse()
            {
                Id = from.Id,
                JsonRPC = from.JsonRPC,
                Result = from.Result,
                Error = error
            };
        }
    }
}