using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Api.Model
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetEmployees();
        public Task<Employee?> GetEmployee(int employeeId);
        public Task<EmployeeAttendance?> MarkAttendances([FromForm] RequestData requestData);
        public Task<Employee> CreateEmployee(Employee employee);
        


    }
}
