using Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace Lab1
{
    public class PutHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var req = context.Request;
            var res = context.Response;

            res.ContentType = "application/json";

            var result = new Result { result = 0, stack = new Stack<int>() };

            if (context.Session[LabsOptions.SessionDataName] is Result data)
            {
                result = data;
            }

            int parameter;

            if (int.TryParse(req.Params["add"], out parameter))
            {
                result.stack.Push(parameter);
                result.result = CalcHelper.CalcResultAsCurrentAndFirst(result);

                context.Session.Add(LabsOptions.SessionDataName, result);
            }

            res.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
