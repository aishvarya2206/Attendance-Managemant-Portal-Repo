using AttendanceManagementPortal.Model;

namespace AttendanceManagementPortal.Web.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetDepartment();
    }
}
