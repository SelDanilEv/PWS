using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using Infrastructure;
using Newtonsoft.Json;

namespace Lab1
{
    public class PostHandler : IHttpHandler, IRequiresSessionState
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

            if (int.TryParse(req.Params["result"], out parameter))
            {
                result.result = parameter;

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
