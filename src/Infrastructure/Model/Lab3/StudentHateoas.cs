using System;
using System.Collections.Generic;

namespace Infrastructure.Model.Lab3
{
    [Serializable]
    public class StudentHateoas : Student 
    {
        private List<HATEOAS> hateoasList;

        public List<HATEOAS> HATEOASList { get => hateoasList; set => hateoasList = value; }

        public StudentHateoas(List<HATEOAS> hATEOAS, Student student)
        {
            HATEOASList = hATEOAS;
            base.Id = student.Id;
            base.Name = student.Name;
            base.Phone = student.Phone;
        }

    }
}
