using AttendanceManagementPortal.Model;

namespace AttendanceManagementPortal.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly  HttpClient _httpClient;
        public EmployeeService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            var result =  await _httpClient.GetFromJsonAsync<Employee[]>($"api/Employees");
            return result;
        }

      
    }
}
