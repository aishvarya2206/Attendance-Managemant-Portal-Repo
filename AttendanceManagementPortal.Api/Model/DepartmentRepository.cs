using AttendanceManagementPortal.Api.Data;
using AttendanceManagementPortal.Model;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementPortal.Api.Model
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private AppDbContext _appDbContext { get; set; }
        
        public DepartmentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Department>> GetDepartment()
        {
            return await _appDbContext.Departments.ToListAsync();
        }
    }
}
