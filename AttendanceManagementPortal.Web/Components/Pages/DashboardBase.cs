using AttendanceManagementPortal.Model;
using AttendanceManagementPortal.Web.Services;
using Microsoft.AspNetCore.Components;

namespace AttendanceManagementPortal.Web.Components.Pages
{
    public class DashboardBase : ComponentBase
    {
        [Inject]
        public required IEmployeeService EmployeeService { get; set; }
        public required IEnumerable<EmployeeAttendance> EmployeeAttendances { get; set; }
        
        protected override async Task OnInitializedAsync()
        {
            EmployeeAttendances = (await EmployeeService.GetEmployeeAttendance()).ToList();

        }

    }
}
