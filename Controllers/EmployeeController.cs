using Employee.DTOs;
using Employee.Repo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _service;
        public EmployeeController(IEmployeeRepository service)
        {
            _service = service;
        }
        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var details = _service.GetById(id);
            return details == null ? NotFound() : Ok(details);
        }
        [HttpPost]
        public IActionResult Create(EmployeeModel employee)
        {
            _service.Add(employee);
            return Ok("Employee added. ");
        }
        [HttpPut]
        public IActionResult Update(EmployeeModel employee)
        {
            _service.Update(employee);
            return Ok("Employee Updated ...");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("Employee Deleted...");
        }
    }
}
