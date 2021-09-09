using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab2.Services
{
    public class SessionService
    {
        public T GetSessionData<T>(string dataName) where T: new()
        {
            return HttpContext.Current.Session?[dataName] is T data ? data : new T(); 
        }

        public void SaveSessionData(string dataName, object model)
        {
            HttpContext.Current.Session?.Add(dataName, model);
        }
    }
}