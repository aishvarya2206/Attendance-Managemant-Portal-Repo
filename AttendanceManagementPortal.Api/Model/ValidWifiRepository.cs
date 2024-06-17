using AttendanceManagementPortal.Api.Data;
using AttendanceManagementPortal.Model;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementPortal.Api.Model
{
    public class ValidWifiRepository : IValidWifiRepository
    {
        private AppDbContext _appDbContext;
        public ValidWifiRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<ValidWiFi?> GetValidWiFiByIDAsync(int wifiId)
        {
            return await _appDbContext.ValidWiFis
                .FirstOrDefaultAsync(x => x.ID == wifiId);
        }

        public async Task<IEnumerable<ValidWiFi>> GetValidWiFisAsync()
        {
            return await _appDbContext.ValidWiFis.ToListAsync();
        }
    }
}
