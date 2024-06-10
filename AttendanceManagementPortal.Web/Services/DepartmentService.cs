using AttendanceManagementPortal.Model;

namespace AttendanceManagementPortal.Web.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _httpClient;
        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Department>> GetDepartment()
        {
            var result = await _httpClient.GetFromJsonAsync<Department[]>($"api/Department");
            return result;
        }
    }
}
