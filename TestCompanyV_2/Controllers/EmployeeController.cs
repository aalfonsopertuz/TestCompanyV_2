using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestCompanyV_2.Domain;
using TestCompanyV_2.Models;

namespace TestCompanyV_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IDEmployee _employee;
        public EmployeeController(IDEmployee employee)
        {
            _employee= employee;
        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                List<Employee> employees = await _employee.GetEmployees();
                return new JsonResult(employees);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                Employee employee = await _employee.GetEmployeeById(id);
                return new JsonResult(employee);
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
