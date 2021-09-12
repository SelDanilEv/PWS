using Infrastructure.Helpers;
using Infrastructure.Model.Lab3;
using Infrastructure.Option;
using Services;
using Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lab3.Controllers
{
    public class StudentController : ApiController
    {
        private IService<Student> _studentService;

        public StudentController()
        {
            var appPath = System.Web.Hosting.HostingEnvironment.MapPath("~");

            var mongoOptions = new MongoOptions()
            {
                ConnectionString = PasswordManagerHelper.GetValueFromTxtFile(appPath, MongoOptions.MongoConnectionString),
                CollectionPrefix = "Lab3_"
            };

            _studentService = new CommonService<Student>(mongoOptions);

            Task.Run(async ()=> await _studentService.AddItem(new Student()));
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
