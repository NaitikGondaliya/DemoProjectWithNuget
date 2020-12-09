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
    public class EmployeeController : BaseController
    {
        IConfiguration _configuration;
        private readonly GenericDbContext _dbContext;

        public EmployeeController(IEmployee employee, GenericDbContext dbContext, IConfiguration configuration)
        {
            Employee = employee;
            _dbContext = dbContext;
            _configuration = configuration;
            ConnectionTools.ChangeDbConnection(_dbContext, _configuration.GetConnectionString("TestDefaultConnection"));
        }


        [HttpPost]
        public IActionResult Add(EmployeeModel employeeModel)
        {
            return Ok(Employee.Add(employeeModel));
        }


        [HttpPut]
        public IActionResult Update(EmployeeModel employeeModel)
        {
            return Ok(Employee.Update(employeeModel));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Employee.GetAllWithFilter());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return Ok(Employee.GetById(id));

        }
        [HttpGet("Name/{Name}")]
        public IActionResult GetByName(string Name)
        {
            return Ok(Employee.GetByColoumn(Name));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return Ok(Employee.Delete(id));
        }

        [HttpPost("Transaction")]
        public IActionResult Add(EmployeeRequestModel employeeModel)
        {
            return Ok(Employee.Add(employeeModel));
        }

        [HttpPut("Transaction")]
        public IActionResult Update(EmployeeRequestModel employeeModel)
        {
            return Ok(Employee.Update(employeeModel));
        }

        [HttpGet("{page:int}/{pageSize:int}")]
        public IActionResult GetAll(int page, int pageSize)
        {
            return Ok(Employee.GetPaginatedData(page, pageSize));
        }

        [HttpGet("ByQuery")]
        public IActionResult GetAllQuery()
        {
            return Ok(Employee.GetByQuery());
        }

        [HttpGet("FillEmployee")]
        public IActionResult FillEmployee()
        {
            return Ok(Employee.FillEmployee());
        }


        [HttpGet("CheckENQ")]
        public IActionResult CheckENQ()
        {
            return Ok(Employee.AddUsingQuery());

        }
        
        
        [HttpGet("checkReadIgnore")]
        public IActionResult checkReadIgnore()
        {
            return Ok(Employee.checkReadIgnore());
        }
    }
}