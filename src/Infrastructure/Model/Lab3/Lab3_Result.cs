using Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure.Model.Lab3
{
    public class Lab3_Result
    {
        public Lab3_Result()
        {
            Students = new List<StudentHateoas>();
        }

        public List<StudentHateoas> Students { get; set; }
    }
}