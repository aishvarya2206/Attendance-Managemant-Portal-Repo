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
        
        // Method - POST
        [HttpPost]
        public async Task<ActionResult> MarkAttendance([FromForm] RequestData requestData)
        {
            try
            {
                var result = await _employeeRepository.MarkAttendances(requestData);
                var response = new
                {
                    Success = true,
                    Message = $"User {requestData.WifiSsid} created successfully!",
                    Code = StatusCodes.Status200OK
                };
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in retrieving database");
            }
        }

    }
}
