using AttendanceManagementPortal.Api.Model;
using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class EmployeeCreateController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeCreateController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        // Method - POST
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
