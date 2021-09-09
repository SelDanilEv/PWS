using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class UserErrorMessagesService
    {
        public string GetErrorMessage(ErrorMessageType messageType, params string[] parameters)
        {
            var result = string.Empty;

            switch (messageType)
            {
                case ErrorMessageType.CheckParam:
                    result = "Check please parameter {0} again";
                    break;
                case ErrorMessageType.EmptyStack:
                    result = "Stack is empty";
                    break;
                case ErrorMessageType.MessageOnly:
                default:
                    result = "{0}";
                    break;
            }

            result = string.Format(result, parameters);

            return result;
        }
    }
}
