using System.Collections.Generic;
using System.Web;
using System.Web.Http;

using Infrastructure;
using Infrastructure.Model;
using Infrastructure.Services;

using Lab2.Services;


namespace Lab2.Controllers
{
    public class StackController : ApiController
    {
        private SessionService _sessionService;

        public StackController()
        {
            _sessionService = new SessionService();
        }

        public IHttpActionResult Get()
        {
            var resultModel = _sessionService.GetSessionData<Result>(LabOptions.SessionDataName);

            return Json(resultModel.ToDto());
        }

        public IHttpActionResult Post([FromBody] int result)
        {
            var resultModel = _sessionService.GetSessionData<Result>(LabOptions.SessionDataName);

            resultModel.result = result;

            return Json(resultModel.ToDto());
        }

        public IHttpActionResult Put([FromBody] int add)
        {
            var resultModel = _sessionService.GetSessionData<Result>(LabOptions.SessionDataName);

            Result.stack.Push(add);
            resultModel.result = CalcHelper.CalcResultAsCurrentAndFirst(resultModel);

            _sessionService.SaveSessionData(LabOptions.SessionDataName, resultModel);

            return Json(resultModel.ToDto());
        }

        public IHttpActionResult Delete()
        {
            var resultModel = _sessionService.GetSessionData<Result>(LabOptions.SessionDataName);

            if (Result.stack.Count > 0)
            {
                Result.stack.Pop();
                resultModel.result = CalcHelper.CalcResultAsCurrentAndFirst(resultModel);
            }
            else
            {
                resultModel = new Result() { statusMessage = new UserErrorMessagesService().GetErrorMessage(ErrorMessageType.EmptyStack) };
            }

            return Json(resultModel.ToDto());
        }
    }
}
