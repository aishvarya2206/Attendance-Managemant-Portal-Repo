using AttendanceManagementPortal.Model;
using AttendanceManagementPortal.Web.Services;
using Microsoft.AspNetCore.Components;

namespace AttendanceManagementPortal.Web.Components.Pages
{
    public class DashboardBase : ComponentBase
    {
        [Inject]
        public required IEmployeeAttendanceService EmployeeAttendanceService { get; set; }
        public required IEnumerable<EmployeeAttendance> EmployeeAttendances { get; set; }
        public AttendanceLog? empLocation { get; set; } = new();
        
        protected override async Task OnInitializedAsync()
        {
           EmployeeAttendances = (await EmployeeAttendanceService.GetEmployeeAttendanceFromBiometric()).ToList();

        }

    }
}
