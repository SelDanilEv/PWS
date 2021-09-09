using Infrastructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure.Dto
{
    public class ResultDto
    {
        public Stack<int> stack { get; set; }
        public int result { get; set; }
        public string statusMessage { get; set; }

        public ResultDto()
        {
            stack = new Stack<int>();
        }

        public ResultDto(Result result)
        {
            this.stack = Result.stack;
            this.result = result.result;
            this.statusMessage = result.statusMessage;
        }
    }
}