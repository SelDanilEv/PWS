using Infrastructure.Enum;
using Infrastructure.Helpers;
using Infrastructure.Model.Lab3;
using Infrastructure.Model.MongoFluentOptions;
using Infrastructure.Option;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        }

        // GET api/student
        public async Task<IHttpActionResult> Get([FromUri] GetStudentListOptions options)
        {
            Lab3_Result result = new Lab3_Result();
            var fluentOptions = new MongoFluentOptions<Student>();

            if (options.SortByName)
            {
                fluentOptions.options.Add(new MongoFluentOptions<Student>.Option("sort", "{Name:1}"));
            }
            else
            {
                fluentOptions.options.Add(new MongoFluentOptions<Student>.Option("sort", "{Id:1}"));
            }
            if (options.Offset != 0)
            {
                fluentOptions.options.Add(new MongoFluentOptions<Student>.Option("skip", options.Offset.ToString()));
            }
            if (options.Limit != 0)
            {
                fluentOptions.options.Add(new MongoFluentOptions<Student>.Option("limit", options.Limit.ToString()));
            }


            try
            {
                if (options.ThrowException)
                {
                    throw new ApplicationException("Sample Exaption");
                }

                IList<Student> students = await _studentService.GetItems(fluentOptions);

                if (!string.IsNullOrWhiteSpace(options.Like))
                {
                    students = students.Where(s => s.Name.ToLower().Contains(options.Like.ToLower())).ToList();
                }
                if (!string.IsNullOrWhiteSpace(options.GlobalLike))
                {
                    students = students.Where(s =>
                        s.Name.ToLower().Contains(options.GlobalLike.ToLower()) ||
                        s.Id.ToLower().Contains(options.GlobalLike.ToLower()) ||
                        s.Phone.ToLower().Contains(options.GlobalLike.ToLower())
                    ).ToList();
                }

                foreach (var student in students)
                {
                    if (!string.IsNullOrWhiteSpace(options.Columns))
                    {
                        var hidden = "Hidden";
                        if (!options.Columns.ToLower().Contains("id"))
                        {
                            student.Id = hidden;
                        }
                        if (!options.Columns.ToLower().Contains("name"))
                        {
                            student.Name = hidden;
                        }
                        if (!options.Columns.ToLower().Contains("phone"))
                        {
                            student.Phone = hidden;
                        }
                    }

                    var hateoases = new List<HATEOAS>()
                    {
                        new HATEOAS(){
                        Href = "api/student",
                        Ref = "Change Student",
                        Method = HttpMethod.Put.Method
                        },
                        new HATEOAS(){
                        Href = $"api/student?id={student.Id}",
                        Ref = "Delete Student",
                        Method = HttpMethod.Delete.Method
                        }
                    };
                    var studentHateoas = new StudentHateoas(hateoases, student);

                    result.Students.Add(studentHateoas);
                }
            }
            catch (Exception e)
            {
                var errorResult = new Lab3_ErrorResult();
                errorResult.HATEOAS = new HATEOAS()
                {
                    Href = $"home/error?message={e.Message}",
                    Ref = "Show error message",
                    Method = HttpMethod.Get.Method
                };
                return BuildResult(options.ContentType, errorResult, HttpStatusCode.BadRequest);
            }

            return BuildResult(options.ContentType, result);
        }

        // POST api/student
        public async Task<IHttpActionResult> Post([FromBody] Student newStudent)
        {
            try
            {
                await _studentService.AddItem(newStudent);
            }
            catch (Exception e)
            {
                var errorResult = new Lab3_ErrorResult();
                errorResult.HATEOAS = new HATEOAS()
                {
                    Href = $"home/error?message={e.Message}",
                    Ref = "Show error message",
                    Method = HttpMethod.Get.Method
                };
                return BuildResult(ResultContentType.JSON, errorResult, HttpStatusCode.BadRequest);
            }
            return Ok();
        }

        // PUT api/student
        public async Task<IHttpActionResult> Put([FromBody] Student updatedStudent)
        {
            try
            {
                await _studentService.UpdateItem(updatedStudent);
            }
            catch (Exception e)
            {
                var errorResult = new Lab3_ErrorResult();
                errorResult.HATEOAS = new HATEOAS()
                {
                    Href = $"home/error?message={e.Message}",
                    Ref = "Show error message",
                    Method = HttpMethod.Get.Method
                };
                return BuildResult(ResultContentType.JSON, errorResult, HttpStatusCode.BadRequest);
            }

            return Ok();
        }

        // DELETE api/student
        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                await _studentService.RemoveItem(id);
            }
            catch (Exception e)
            {
                var errorResult = new Lab3_ErrorResult();
                errorResult.HATEOAS = new HATEOAS()
                {
                    Href = $"home/error?message={e.Message}",
                    Ref = "Show error message",
                    Method = HttpMethod.Get.Method
                };
                return BuildResult(ResultContentType.JSON, errorResult, HttpStatusCode.BadRequest);
            }

            return Ok();
        }

        private IHttpActionResult BuildResult<T>(ResultContentType contentType, T model, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            switch (contentType)
            {
                case ResultContentType.XML:
                    return Content(statusCode, model, Configuration.Formatters.XmlFormatter);
                case ResultContentType.JSON:
                default:
                    return Content(statusCode, model, Configuration.Formatters.JsonFormatter);
            }
        }
    }
}
