using Infrastructure;
using Infrastructure.Model;
using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;

namespace Lab1
{
    public class GetHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var res = context.Response;

            res.ContentType = "application/json";

            var result = new Result { result = 0};

            if (context.Session[LabOptions.SessionDataName] is Result data)
            {
                result = data;
            }

            res.Write(JsonConvert.SerializeObject(result.ToDto()));
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
