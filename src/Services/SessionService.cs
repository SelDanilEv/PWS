using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services
{
    public class SessionService
    {
        public T GetSessionData<T>(string dataName)
        {
            return HttpContext.Current.Session?[dataName] is T data ? data : default;
        }

        public void SetSessionData(string dataName, object model)
        {
            HttpContext.Current.Session?.Add(dataName, model);
        }

        public void ClearSession()
        {
            HttpContext.Current.Session?.Clear();
        }
    }
}
