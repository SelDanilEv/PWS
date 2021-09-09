using System.Collections.Generic;
using System.Web;
using System.Web.SessionState;
using Infrastructure;
using Infrastructure.Model;
using Infrastructure.Services;
using Newtonsoft.Json;

namespace Lab1
{
    public class PostHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            int parameter;
            var parameterName = "result";

            var req = context.Request;
            var res = context.Response;

            res.ContentType = "application/json";

            var result = new Result { result = 0 };

            if (context.Session[LabOptions.SessionDataName] is Result data)
            {
                result = data;
            }


            if (int.TryParse(req.Params[parameterName], out parameter))
            {
                result.result = parameter;

                context.Session.Add(LabOptions.SessionDataName, result);
            }
            else
            {
                result = new Result()
                {
                    statusMessage = new UserErrorMessagesService().
                        GetErrorMessage(ErrorMessageType.CheckParam, parameterName)
                };
            }

            res.Write(JsonConvert.SerializeObject(result.ToDto()));
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
