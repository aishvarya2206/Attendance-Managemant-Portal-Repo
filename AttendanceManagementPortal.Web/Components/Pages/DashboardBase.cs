using AttendanceManagementPortal.Model;
using AttendanceManagementPortal.Web.Components.Account.Pages.Manage;
using AttendanceManagementPortal.Web.Data;
using AttendanceManagementPortal.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AttendanceManagementPortal.Web.Components.Pages
{
    public class DashboardBase : ComponentBase
    {
        [Inject]
        public required IEmployeeAttendanceService EmployeeAttendanceService { get; set; }
        public required IEnumerable<EmployeeAttendance> EmployeeAttendances { get; set; }
        [Inject]
        public AuthenticationStateProvider authenticationStateProvider { get; set; }
        public AttendanceLog? empLocation { get; set; } = new();
        [Parameter]
        public string employeeid { get; set; } = "0";

        private int _employeeid;

        protected override async Task OnInitializedAsync()
        {
            if (int.TryParse(employeeid, out _employeeid) && _employeeid != 0)
            {
                EmployeeAttendances = (await EmployeeAttendanceService.GetEmployeeAttendanceByEmployeeId(_employeeid));
            }
            else
            {
                var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
                var emailId = authState.User.Identity.Name;
                EmployeeAttendances = (await EmployeeAttendanceService.GetEmployeeAttendanceForEmployee(emailId)).ToList();
            }
            

        }

    }
}
