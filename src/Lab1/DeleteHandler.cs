using Infrastructure.Enum;
using Infrastructure;
using Newtonsoft.Json;
using System.Web;
using System.Web.SessionState;
using Infrastructure.Model;
using Services;

namespace Lab1
{
    public class DeleteHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            var res = context.Response;

            res.ContentType = "application/json";

            var result = new Result { result = 0 };

            if (context.Session[LabOptions.SessionDataName] is Result data)
            {
                result = data;
            }

            if (Result.stack.Count > 0)
            {
                Result.stack.Pop();
                result.result = CalcHelper.CalcResultAsCurrentAndFirst(result);
            }
            else
            {
                result = new Result() { statusMessage = new UserErrorMessagesService().GetErrorMessage(ErrorMessageType.EmptyStack) };
            }

            res.Write(JsonConvert.SerializeObject(result.ToDto()));
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
