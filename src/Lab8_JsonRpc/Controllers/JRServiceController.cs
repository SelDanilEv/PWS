using Infrastructure.Enum;
using Lab8_JsonRpc.Models;
using Newtonsoft.Json.Linq;
using Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace Lab8_JsonRpc.Controllers
{
    [System.Web.Mvc.SessionState(SessionStateBehavior.Required)]
    public class JRServiceController : ApiController
    {
        private class JSServiceResults
        {
            public JsonRPCErrorResponse[] JsonRPCErrorResponses { get; set; }
            public JsonRPCResponse[] JsonRPCResponses { get; set; }
        }

        private readonly SessionService _sessionService;
        private const string BlockedSessionKey = "IsSessionBlocked";

        private ConcurrentBag<JsonRPCResponse> _resultCollection;
        private ConcurrentBag<JsonRPCErrorResponse> _errorResultCollection;
        private HttpContext _currentHttpContext;

        public JRServiceController()
        {
            _sessionService = new SessionService();
            _resultCollection = new ConcurrentBag<JsonRPCResponse>();
            _errorResultCollection = new ConcurrentBag<JsonRPCErrorResponse>();
        }

        [HttpPost]
        public object Single(object input, bool isParallel = true)
        {
            if (_sessionService.GetSessionData<bool>(BlockedSessionKey))
            {
                return Json(JsonRPCErrorResponse.FromCommonResponse(
                        JsonRPCResponse.Empty, JsonRPCError.ServerBlocked));
            }

            object request = null;
            Lab8ResponseType responseType = Lab8ResponseType.Invalid;

            var token = JToken.Parse(input.ToString());

            if (token is JArray)
            {
                try
                {
                    request = token.ToObject<ICollection<JsonRPCRequest>>();
                    var jsonRPCRequests = request as ICollection<JsonRPCRequest>;
                    _currentHttpContext = HttpContext.Current;

                    Stopwatch stopwatch = new Stopwatch();

                    if (isParallel)
                    {
                        stopwatch.Start();
                        ParallelLoopResult parallelForEachResult =
                            Parallel.ForEach<JsonRPCRequest>(jsonRPCRequests,
                            ProccessRPCRequest);
                        stopwatch.Stop();
                        Console.WriteLine("Parallel");
                        Console.WriteLine(stopwatch.ElapsedTicks);
                    }
                    else
                    {
                        stopwatch.Reset();
                        stopwatch.Start();
                        foreach (var item in jsonRPCRequests)
                        {
                            ProccessRPCRequest(item);
                        }
                        stopwatch.Stop();
                        Console.WriteLine("Not Parallel");
                        Console.WriteLine(stopwatch.ElapsedTicks);
                    }


                    responseType = Lab8ResponseType.Array;
                }
                catch { }
            }
            else if (token is JObject)
            {
                try
                {
                    request = token.ToObject<JsonRPCRequest>();
                    var jsonRPCRequest = request as JsonRPCRequest;
                    ProccessRPCRequest(jsonRPCRequest);
                    responseType = Lab8ResponseType.SingleObject;
                }
                catch { }
            }

            if (request == null)
            {
                return Json(new JsonRPCErrorResponse()
                {
                    Id = "-1",
                    Error = JsonRPCError.InvalidParams
                });
            }

            if (responseType == Lab8ResponseType.SingleObject)
            {
                if (_errorResultCollection.Count > 0)
                    return Json(_errorResultCollection.FirstOrDefault());
                return Json(_resultCollection.FirstOrDefault());
            }
            else
            {
                return Json(new JSServiceResults()
                {
                    JsonRPCErrorResponses = _errorResultCollection.ToArray(),
                    JsonRPCResponses = _resultCollection.ToArray()
                });
            }
        }

        public void ProccessRPCRequest(JsonRPCRequest jsonRPCRequest)
        {
            if (_currentHttpContext != null)
            {
                HttpContext.Current = _currentHttpContext;
            }

            var response = new JsonRPCResponse();
            response.Id = jsonRPCRequest.Id;
            response.JsonRPC = jsonRPCRequest.JsonRPC;

            try
            {
                var requestData = jsonRPCRequest.Params;
                var result = new Dictionary<string, string>();

                if (requestData != null)
                {
                    foreach (var data in requestData)
                    {
                        var keyValue = new KeyValuePair<string, int>();
                        switch (jsonRPCRequest.Method)
                        {
                            case JsonRPCMethod.SetM:
                                keyValue = SetM(data.Key, int.Parse(data.Value.ToString()));
                                break;
                            case JsonRPCMethod.GetM:
                                keyValue = GetM(data.Key);
                                break;
                            case JsonRPCMethod.AddM:
                                keyValue = AddM(data.Key, int.Parse(data.Value.ToString()));
                                break;
                            case JsonRPCMethod.SubM:
                                keyValue = SubM(data.Key, int.Parse(data.Value.ToString()));
                                break;
                            case JsonRPCMethod.MulM:
                                keyValue = MulM(data.Key, int.Parse(data.Value.ToString()));
                                break;
                            case JsonRPCMethod.DivM:
                                keyValue = DivM(data.Key, int.Parse(data.Value.ToString()));
                                break;
                            case JsonRPCMethod.ErrorExit:
                                keyValue = new KeyValuePair<string, int>(string.Empty, 0);
                                _errorResultCollection.Add(JsonRPCErrorResponse.FromCommonResponse(response, JsonRPCError.ServerBlocked));
                                ErrorExit();
                                break;
                            default:
                                keyValue = new KeyValuePair<string, int>(string.Empty, 0);
                                _errorResultCollection.Add(JsonRPCErrorResponse.FromCommonResponse(response, JsonRPCError.MethodNotFound));
                                break;
                        }

                        result.Add(keyValue.Key, keyValue.Value.ToString());
                    }
                }

                response.Result = result;

                _resultCollection.Add(response);
            }
            catch (Exception ex)
            {
                _errorResultCollection.Add(JsonRPCErrorResponse.FromCommonResponse(response,
                    JsonRPCError.UnexpectedError(ex.Message)));
            }
        }

        private KeyValuePair<string, int> SetM(string key, int value)
        {
            _sessionService.SetSessionData(key, value);

            return GetM(key);
        }

        private KeyValuePair<string, int> GetM(string key)
        {
            var value = _sessionService.GetSessionData<int>(key);

            var result = new KeyValuePair<string, int>(key, value);

            return result;
        }

        private KeyValuePair<string, int> AddM(string key, int value)
        {
            int currentValue = GetM(key).Value;
            _sessionService.SetSessionData(key, currentValue + value);
            return GetM(key);
        }

        private KeyValuePair<string, int> SubM(string key, int value)
        {
            int currentValue = GetM(key).Value;
            _sessionService.SetSessionData(key, currentValue - value);
            return GetM(key);
        }

        private KeyValuePair<string, int> MulM(string key, int value)
        {
            int currentValue = GetM(key).Value;
            _sessionService.SetSessionData(key, currentValue * value);
            return GetM(key);
        }

        private KeyValuePair<string, int> DivM(string key, int value)
        {
            int currentValue = GetM(key).Value;
            _sessionService.SetSessionData(key, currentValue / value);
            return GetM(key);
        }

        private void ErrorExit()
        {
            _sessionService.ClearSession();
            _sessionService.SetSessionData(BlockedSessionKey, true);
        }
    }
}