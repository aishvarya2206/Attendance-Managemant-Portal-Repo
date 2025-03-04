﻿using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Web.Services
{
    public interface IEmployeeAttendanceService
    {
        Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendance();
        Task<AttendanceLog> GetAttendanceByEmployeeIdLastUpdate(int empid);
        Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendanceForEmployee(string email);
        Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendanceByEmployeeId(int employeeid);
    }
}
