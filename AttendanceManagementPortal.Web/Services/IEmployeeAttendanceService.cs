using AttendanceManagementPortal.Model;

namespace AttendanceManagementPortal.Web.Services
{
    public interface IEmployeeAttendanceService
    {
        Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendance();
        Task<AttendanceLog> GetAttendanceByEmployeeIdLastUpdate(int empid);
        Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendanceForEmployee(string email);
    }
}
