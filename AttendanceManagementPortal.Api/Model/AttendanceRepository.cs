using AttendanceManagementPortal.Api.Data;
using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceManagementPortal.Api.Model
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private  AppDbContext _appDbContext {  get; set; }
        public AttendanceRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<AttendanceLog> CreateAttendanceLog([FromForm] RequestData requestData)
        {

            try
            {
                var attendanceLog = new AttendanceLog();

                // When connect to wifi 
                if (requestData.WifiSsid != null)
                {
                    var checkwifi = await _appDbContext.ValidWiFis.FirstOrDefaultAsync(w => w.SSID == requestData.WifiSsid);
                    if (checkwifi != null)
                    {
                        Employee? employee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.DeviceIPAddress == requestData.IpAddress);
                        if (employee != null)
                        {
                            attendanceLog.Employee = employee;
                            attendanceLog.Date = DateTime.Today;
                            attendanceLog.Type = "In";
                            attendanceLog.Source = checkwifi.ID.ToString();
                            attendanceLog.Time = DateTime.Today.Add(DateTime.Now.TimeOfDay);


                            _appDbContext.AttendanceLogs.Add(attendanceLog);
                            await _appDbContext.SaveChangesAsync();

                        }
                    }
                }
                //when disconnect to wifi
                else
                {
                    Employee? employee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.DeviceIPAddress == requestData.IpAddress);

                    attendanceLog.Employee = employee;
                    attendanceLog.Date = DateTime.Today;
                    attendanceLog.Type = "Out";
                    attendanceLog.Source = "";
                    attendanceLog.Time = DateTime.Today.Add(DateTime.Now.TimeOfDay);


                    var Attendanceresult = _appDbContext.AttendanceLogs.Add(attendanceLog);
                    await _appDbContext.SaveChangesAsync();
                    if(Attendanceresult != null)
                    {
                        // update check out time of today in employee attendance table 

                        var EmployeeTodayresult = await _appDbContext.EmployeesAttendances
                                .Where(y => y.Date == DateTime.Today)
                                .FirstOrDefaultAsync(x => x.EmployeeID == employee.ID);
                        if(EmployeeTodayresult != null)
                        {
                            EmployeeTodayresult.CheckOut = DateTime.Now.ToLongTimeString();
                            await _appDbContext.SaveChangesAsync();
                        }
                        
                    }
                }
                return attendanceLog;


            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message: {0}", e.Message);
                Console.WriteLine("Stack Trace: {0}", e.StackTrace);
                throw;
            }

        }
        

        public async Task<IEnumerable<AttendanceLog>> GetAttendanceLogByEmployeeID(int employeeID)
        {
            var result = await _appDbContext.AttendanceLogs
                .Where(x => x.EmployeeID == employeeID)
                .Include(y => y.Employee)
                .ToListAsync();
            return result;
        }

        public async Task<IEnumerable<AttendanceLog>> GetAttendanceLogsAsync()
        {
            var result = await _appDbContext.AttendanceLogs
                .Include(x => x.Employee)
                .ToListAsync();
            return result;
        }

        public async Task<AttendanceLog> GetAttendanceByEmployeeIdLastUpdate(int id)
        {
            var result = await _appDbContext.AttendanceLogs
                .Where(x => x.EmployeeID == id)
                .Include(y => y.Employee)
                .OrderByDescending(x => x.ID)
                .FirstOrDefaultAsync();
            return result;
        }

        
    }
}
