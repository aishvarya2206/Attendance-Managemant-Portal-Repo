using AttendanceManagementPortal.Model;

namespace AttendanceManagementPortal.Web.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendance();
    }
}
