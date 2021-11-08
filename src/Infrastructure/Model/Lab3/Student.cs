using Infrastructure.Models;
using System;

namespace Infrastructure.Model.Lab3
{
    [Serializable]
    public class Student : BaseModel
    {
        private string name;
        private string phone;

        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }

        public Student() { }
        public Student(string name, string phone)
        {
            Name = name;
            Phone = phone;
        }
    }
}
