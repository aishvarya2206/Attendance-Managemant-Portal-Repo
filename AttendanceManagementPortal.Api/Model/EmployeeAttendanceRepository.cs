using AttendanceManagementPortal.Api.Data;
using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementPortal.Api.Model
{
    public class EmployeeAttendanceRepository : IEmployeeAttendanceRepository
    {
        private AppDbContext _appDbContext { get; set; }
        public EmployeeAttendanceRepository(AppDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<EmployeeAttendance>> GetAttendances()
        {
            return await _appDbContext.EmployeesAttendances
                .Include(x => x.Employee)
                .Include(y => y.Employee.Department)
                .ToListAsync();
        }
    }
}
