using Lab8_JsonRpc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.SessionState;
using Newtonsoft.Json;

namespace Lab8_JsonRpc.Controllers
{
    [SessionState(SessionStateBehavior.Required)]
    public class JRServiceController : ApiController
    {
        private static bool ignoreMethods = false;

        [System.Web.Http.HttpPost]
        public object Single(ReqJsonRPC body)
        {
            if (ignoreMethods)
            return Json(GetError(body.Id, body.JsonRPC, new ErrorJsonRPC { Message = "Methods are not available", Code = -32601 }));

            string method = body.Method;
            DataModel param = body.Params;
            string key = param.Key;
            int value = int.Parse(param.Value == null || param.Value == "" ? "0" : param.Value); // default value = 0
            int? result = null;

            switch (method)
            {
                case "SetM":
                    {
                        result = SetM(key, value);
                        break;
                    }
                case "GetM":
                    {
                        result = GetM(key);
                        break;
                    }
                case "AddM":
                    {
                        result = AddM(key, value);
                        break;
                    }
                case "SubM":
                    {
                        result = SubM(key, value);
                        break;
                    }
                case "MulM":
                    {
                        result = MulM(key, value);
                        break;
                    }
                case "DivM":
                    {
                        result = DivM(key, value);
                        break;
                    }
                case "ErrorExit":
                    {
                        ErrorExit();
                        break;
                    }
                default:
                    {
                        return Json(GetError(body.Id, body.JsonRPC, new ErrorJsonRPC { Message = string.Format("Function {0} is not found", body.Method), Code = -32601 }));
                    }
            }
            return Json(new ResJsonRPC()
            {
                Id = body.Id,
                JsonRPC = body.JsonRPC,
                Method = body.Method,
                Result = result
            }
            );
        }
        ////////////////////////////////////
        private ResJsonRPCError GetError(string id, string jsonRPC, ErrorJsonRPC error)
        {
            return new ResJsonRPCError()
            {
                Id = id,
                JsonRPC = jsonRPC,
                Error = error
            };
        }
        ////////////////////////////////////
        private int? SetM(string k, int x)
        {
            HttpContext.Current.Session[k] = x;
            return GetM(k);
        }

        private int? GetM(string k)
        {
            object result = HttpContext.Current.Session[k];
            if (result == null)
                return null;
            else
                return int.Parse(result.ToString());
        }

        private int? AddM(string k, int x)
        {
            int? value = GetM(k);
            HttpContext.Current.Session[k] = value == null ? x : value + x;
            return GetM(k);
        }

        private int? SubM(string k, int x)
        {
            int? value = GetM(k);
            HttpContext.Current.Session[k] = value == null ? x : value - x;
            return GetM(k);
        }

        private int? MulM(string k, int x)
        {
            int? value = GetM(k);
            HttpContext.Current.Session[k] = value == null ? x : value * x;
            return GetM(k);
        }

        private int? DivM(string k, int x)
        {
            int? value = GetM(k);
            HttpContext.Current.Session[k] = value == null ? x : value / x;
            return GetM(k);
        }

        private void ErrorExit()
        {
            HttpContext.Current.Session.Clear();
            //HttpContext.Session["MethodsIgnore"] = true;
            ignoreMethods = true;
        }
    }
}