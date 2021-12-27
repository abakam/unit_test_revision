using dotnetcore_web_api.Data.Models;
using dotnetcore_web_api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcore_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IGenericRepository<Employee> _repo = null;

        public EmployeeController(IGenericRepository<Employee> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetEmployee()
        {
            var model = _repo.GetAll();
            return Ok(model);
        }

        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployeeById(long id)
        {
            Employee employee = _repo.GetById(id);
            if(employee == null)
            {
                return NotFound("The employee record couldn't be found.");
            }
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<Employee> CreateEmployee(Employee employee)
        {
            if(employee == null)
            {
                return BadRequest("Employee is null.");
            }
            _repo.Insert(employee);
            return CreatedAtAction("GetEmployeeById", new { Id = employee.EmployeeId }, employee);
        }

        [HttpPut]
        public IActionResult Put(Employee employee)
        {
            if(employee == null)
            {
                return BadRequest("Employee is null");
            }
            _repo.Update(employee);
            return NoContent();
        }

        public bool CheckIfUserCanBeVoter(int age)
        {
            return (age >= 18) ? true : false;
        }
    }
}
