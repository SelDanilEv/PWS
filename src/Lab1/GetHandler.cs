using Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;

namespace Lab1
{
    public class GetHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var res = context.Response;

            res.ContentType = "application/json";

            var result = new Result { result = 0, stack = new Stack<int>() };

            if (context.Session[LabsOptions.SessionDataName] is Result data)
            {
                result = data;
            }

            res.Write(JsonConvert.SerializeObject(result));
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
