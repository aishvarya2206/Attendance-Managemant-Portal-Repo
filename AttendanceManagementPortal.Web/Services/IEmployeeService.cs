using AttendanceManagementPortal.Model;

namespace AttendanceManagementPortal.Web.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployee();
        Task<bool> CheckEmailAvailabile(string email);
    }
}
