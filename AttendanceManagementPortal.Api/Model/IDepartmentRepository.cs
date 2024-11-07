using AttendanceManagementPortal.Model;

namespace AttendanceManagementPortal.Api.Model
{
    public interface IDepartmentRepository
    {
        public Task<IEnumerable<Department>> GetDepartment();
    }
}
