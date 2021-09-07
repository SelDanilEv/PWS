using System.Collections.Generic;
using System.Linq;

namespace Lab1
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
            var stackArray = result.stack.ToArray();
            return stackArray.FirstOrDefault() + result.result;
        }
    }
}