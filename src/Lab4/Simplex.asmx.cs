using System.Web.Script.Services;
using System.Web.Services;

namespace Lab4
{
    [WebService(Namespace = "http://sde/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    public class Simplex : WebService
    {
        [WebMethod(MessageName = "sum", Description = "Calc sum of 2 int values")]
        public int Add(int x, int y)
        {
            return x + y;
        }

        [WebMethod(MessageName = "concat", Description = "Concatination of string and double")]
        public string Concat(string s, double d)
        {
            return $"{s}{d.ToString()}";
        }

        [WebMethod(MessageName = "sumA", Description = "Sum of two A objects")]
        public A Sum(A a1, A a2)
        {
            return a1 + a2;
        }

        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        [WebMethod(MessageName = "sumjson", Description = "Sum of 2 int. Response JSON")]
        public int Adds(int x, int y)
        {
            return x + y;
        }

        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        [WebMethod(MessageName = "getjsonA", Description = "Example of JSON")]
        public A GetA()
        {
            return new A
            {
                s = "str",
                f = 5.4f,
                k = 4
            };
        }
    }
}
