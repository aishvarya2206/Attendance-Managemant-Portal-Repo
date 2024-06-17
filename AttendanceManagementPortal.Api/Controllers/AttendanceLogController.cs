using AttendanceManagementPortal.Api.Model;
using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AttendanceLogController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        // Employee Attendance Repository for auto update the attendance for the day
        private readonly IEmployeeAttendanceRepository _employeeAttendanceRepository;
        private EmployeeAttendance empAttend { get; set; } = new();
        public AttendanceLogController(IAttendanceRepository attendanceRepository, IEmployeeAttendanceRepository employeeAttendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
            _employeeAttendanceRepository = employeeAttendanceRepository;

        }
        //Url - https://localhost:7095/api/AttendanceLog
        [HttpGet]
        public async Task<ActionResult> GetAttendances() 
        {
            try
            {
                var result = await _attendanceRepository.GetAttendanceLogsAsync();
                return Ok(result);
            }
            catch(Exception) 
            {
                return StatusCode(500, "Error in retrieving database");
            }
           
        }
        [HttpPost]
        public async Task<ActionResult> MarkAttendance([FromForm] RequestData requestData)
        {
            try
            {
                var result = await _attendanceRepository.CreateAttendanceLog(requestData);
                if(result != null) 
                {
                    // Employee Attendance for the day creation in Employee Attendance Table
                    var todayAttendance = await _employeeAttendanceRepository.GetEmployeeAttendanceByEmployeeIdForToday(result.EmployeeID);
                    if (todayAttendance == null)
                    {
                        empAttend.CheckIn = DateTime.Now.ToLongTimeString();
                        empAttend.CheckOut = "";
                        empAttend.Date = DateTime.Today;
                        empAttend.Employee = result.Employee;

                        var empAttendance = await _employeeAttendanceRepository.CreateEmployeeAttendance(empAttend);
                        
                    }

                    //---------------------------------------------
                    var response = new
                    {
                        Success = true,
                        Message = $"User {requestData.WifiSsid} created successfully!",
                        Code = StatusCodes.Status200OK

                    };
                }
                
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in retrieving database");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAttendanceByEmployeeId(int id)
        {
            try 
            {
                var result = await _attendanceRepository.GetAttendanceLogByEmployeeID(id);
                return Ok(result);

            }
            catch (Exception) 
            {
                return StatusCode(500, "Error in retrieving database");
            }
        }
        [HttpGet("search/{id}")]
        public async Task<ActionResult> GetAttendanceByEmployeeIdLastUpdate(int id)
        {
            try
            {
                var result = await _attendanceRepository.GetAttendanceByEmployeeIdLastUpdate(id);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in retrieving database");
            }
        }
    }
}
