using Infrastructure;
using Newtonsoft.Json;
using System.Web;
using Infrastructure.Model;
using System.Web.SessionState;
using Services;
using Infrastructure.Enum;

namespace Lab1
{
    public class PutHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            int parameter;
            var parameterName = "add";

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
                Result.stack.Push(parameter);
                result.result = CalcHelper.CalcResultAsCurrentAndFirst(result);

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
