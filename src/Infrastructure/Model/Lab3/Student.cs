using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Model.Lab3
{
    public class Student : BaseModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
