using AttendanceManagementPortal.Model;
using AttendanceManagementPortal.Web.Components.Account.Pages.Manage;

namespace AttendanceManagementPortal.Web.Services
{
    public class EmployeeAttendanceService : IEmployeeAttendanceService
    {
        private readonly HttpClient _httpClient;
        public EmployeeAttendanceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AttendanceLog> GetAttendanceByEmployeeIdLastUpdate(int empid)
        {
            var result = await _httpClient.GetFromJsonAsync<AttendanceLog>($"api/AttendanceLog/search/{empid}");
            return result;
        }

        public async Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendance()
        {
            var result = await _httpClient.GetFromJsonAsync<EmployeeAttendance[]>($"api/EmployeeAttendance");
            return result;
        }

        public async Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendanceByEmployeeId(int employeeid)
        {
            var result = await _httpClient.GetFromJsonAsync<EmployeeAttendance[]>($"api/EmployeeAttendance/GetEmployeeAttendanceByEmployeeId/{employeeid}");
            return result;
        }

        public async Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendanceForEmployee(string email)
        {
            var result = await _httpClient.GetFromJsonAsync<EmployeeAttendance[]>($"api/EmployeeAttendance/EmployeeAttendanceForEmployee/{email}");
            return result;
        }
    }
}
