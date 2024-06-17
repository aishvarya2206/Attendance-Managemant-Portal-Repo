using AttendanceManagementPortal.Api.Model;
using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        // URI - https://localhost:7036/api/employees
        // Method - GET
        [HttpGet]
        public async Task<ActionResult> GetEmployee()
        {
            try
            {
                var result = await _employeeRepository.GetEmployees();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in retrieving database");
            }

        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] Employee requestData)
        {
            try
            {
                var result = await _employeeRepository.CreateEmployee(requestData);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in retrieving database");
            }
        }

       

    }
}
