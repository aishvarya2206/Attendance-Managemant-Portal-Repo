using AttendanceManagementPortal.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            this._departmentRepository = departmentRepository;
        }
        // URI - https://localhost:7036/api/department
        // Method - GET
        [HttpGet]
        public async Task<ActionResult> GetDepartment()
        {
            try
            {
                var result = await _departmentRepository.GetDepartment();
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in retrieving database");
            }

        }

    }
}
