using Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure.Model
{
    public class Result
    {
        public static Stack<int> stack { get; set; }
        public int result { get; set; }
        public string statusMessage { get; set; }

        public Result()
        {
            //stack ??= new Stack<int>();  Core fast version

            if(stack == null)
            {
                stack = new Stack<int>();
            }
        } 

        public ResultDto ToDto() => new ResultDto(this);
    }
}