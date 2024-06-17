using AttendanceManagementPortal.Model;

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
    }
}
