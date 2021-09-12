using Infrastructure.Model;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure
{
    public static class CalcHelper
    {
        public static int CalcResultAsSum(Stack<int> stack)
        {
            var stackArray = stack.ToArray();
            return stackArray.Sum();
        }

        public static int CalcResultAsCurrentAndFirst(Result result)
        {
            var stackArray = Result.stack.ToArray();
            return stackArray.FirstOrDefault() + result.result;
        }
    }
}