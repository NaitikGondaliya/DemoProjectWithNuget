using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ENP.DA;
using ENP.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShivOhm.Infrastructure;

namespace ExampleWithNuGetPackage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {       

        IConfiguration _configuration;
        private readonly GenericDbContext _dbContext;

        public StudentController(IStudent student, GenericDbContext dbContext, IConfiguration configuration)
        {
            Student = student;
            _dbContext = dbContext;
            _configuration = configuration;
            ConnectionTools.ChangeDbConnection(_dbContext, _configuration.GetConnectionString("TestDefaultConnection"));
        }

        [HttpPost]
        public IActionResult Add(StudentModel student)
        {
            return Ok(Student.Add(student));
        }

        [HttpPut]
        public IActionResult Update(StudentModel student)
        {
            return Ok(Student.Update(student));
        }

        [HttpGet("{studentId}")]
        public IActionResult GetById(long studentId)
        {
            return Ok(Student.GetById(studentId));
        }

        [HttpGet("Details/{studentId}")]
        public IActionResult GetDetailById(long studentId)
        {
            return Ok(Student.GetStudentDetailById(studentId));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Student.GetAll());
        }
        
        [HttpGet("{PageNo}/{PageSize}")]
        [HttpGet("{PageNo}/{PageSize}/{OrderBy}")]
        [HttpGet("{PageNo}/{PageSize}/{OrderBy}/{SearchBy}")]
        public IActionResult GetAll(int PageNo, int PageSize, string OrderBy, string SearchBy)
        {
            return Ok(Student.GetAll(PageNo,PageSize,OrderBy,SearchBy));
        }

        [HttpDelete("{studentId}")]
        public IActionResult Delete(long studentId)
        {
            return Ok(Student.Delete(studentId));
        }

        [HttpPost("WithCourse")]
        public IActionResult InsertWithCourse(StudentWithCourseModel studentWithCourse)
        {
            return Ok(Student.InsertWithCourse(studentWithCourse));
        }

    }
}