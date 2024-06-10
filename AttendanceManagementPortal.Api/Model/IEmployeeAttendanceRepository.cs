using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Api.Model
{
    public interface IEmployeeAttendanceRepository
    {
        public Task<IEnumerable<EmployeeAttendance>> GetAttendances();
    }
}
