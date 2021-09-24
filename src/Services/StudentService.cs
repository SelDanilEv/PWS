using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Interfaces;
using Infrastructure.Models;
using Repository;
using Repository.Interfaces;
using Infrastructure.Option;

namespace Services
{
    public class StudentService<T> : CommonService<T> where T : IBaseModel
    {
        public StudentService(MongoOptions mongoOptions) : base(mongoOptions)
        {
        }
    }
}
