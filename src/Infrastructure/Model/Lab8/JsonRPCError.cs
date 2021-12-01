namespace Lab8_JsonRpc.Models
{
    public class JsonRPCError
    {
        public JsonRPCError(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; set; }
        public string Message { get; set; }

        #region rpc standart errors
        public static JsonRPCError MethodNotFound => new JsonRPCError(-32601, "Method not found");
        public static JsonRPCError InvalidParams => new JsonRPCError(-32602, "Invalid params");
        #endregion

        #region custom errors (from -32000 to -32099 codes)
        public static JsonRPCError ServerBlocked => new JsonRPCError(-32000, "Server was locked. Ignored mode.");
        public static JsonRPCError UnexpectedError(string message) => new JsonRPCError(-32001, $"Unexpected error: {message}");
        #endregion
    }
}