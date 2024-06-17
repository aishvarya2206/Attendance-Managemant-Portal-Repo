using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Api.Model
{
    public interface IAttendanceRepository
    {
        public Task<IEnumerable<AttendanceLog>> GetAttendanceLogsAsync();
        public Task<IEnumerable<AttendanceLog>> GetAttendanceLogByEmployeeID(int EmployeeID);
        public Task<AttendanceLog> CreateAttendanceLog([FromForm] RequestData requestData);
        public Task<AttendanceLog> GetAttendanceByEmployeeIdLastUpdate(int id);
        
    }
}
