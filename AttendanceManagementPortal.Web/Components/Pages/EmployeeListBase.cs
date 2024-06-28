using AttendanceManagementPortal.Model;
using AttendanceManagementPortal.Web.Services;
using Microsoft.AspNetCore.Components;

namespace AttendanceManagementPortal.Web.Components.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeService.GetEmployee();
        }
    }
}
