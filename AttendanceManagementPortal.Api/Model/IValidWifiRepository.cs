using AttendanceManagementPortal.Model;

namespace AttendanceManagementPortal.Api.Model
{
    public interface IValidWifiRepository
    {
        Task<IEnumerable<ValidWiFi>> GetValidWiFisAsync();
        Task<ValidWiFi?> GetValidWiFiByIDAsync(int wifiId);
    }
}
