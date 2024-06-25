using AttendanceManagementPortal.Api.Model;
using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class EmployeeAttendanceController : ControllerBase
    {
        private readonly IEmployeeAttendanceRepository _employeeAttendanceRepository;
        public EmployeeAttendanceController(IEmployeeAttendanceRepository employeeAttendanceRepository)
        {
            this._employeeAttendanceRepository = employeeAttendanceRepository;
        }
        // URI - https://localhost:7095/api/EmployeeAttendance
        // Method - GET
        [HttpGet]
        public async Task<ActionResult> GetEmployeeAttendance()
        {
            try
            {
                var result = await _employeeAttendanceRepository.GetEmployeeAttendance();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in retrieving database");
            }
        }
        [HttpGet("EmployeeAttendanceForEmployee/{email}")]
        public async Task<ActionResult> GetEmployeeAttendanceForEmployee(string email)
        {
            try
            {
                var result = await _employeeAttendanceRepository.GetEmployeeAttendanceForEmployee(email);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in retrieving database");
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateEmployeeAttendance(EmployeeAttendance employeeAttendance)
        {
            try
            {
                var result = await _employeeAttendanceRepository.CreateEmployeeAttendance(employeeAttendance);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in retrieving database");
            }
        }
    }
}
