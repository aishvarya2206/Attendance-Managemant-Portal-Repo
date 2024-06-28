using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Api.Model
{
    public interface IEmployeeAttendanceRepository
    {
        public Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendance();
        public Task<EmployeeAttendance> CreateEmployeeAttendance(EmployeeAttendance employeeAttendance);
        public Task<EmployeeAttendance?> GetEmployeeAttendanceByEmployeeId(int employeeId);
        public Task<EmployeeAttendance?> GetEmployeeAttendanceByEmployeeIdForToday(int employeeId);
        Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendanceFromBiometric();
    }
}
