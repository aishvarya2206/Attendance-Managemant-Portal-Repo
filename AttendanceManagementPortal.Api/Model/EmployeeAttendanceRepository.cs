using AttendanceManagementPortal.Api.Data;
using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;

namespace AttendanceManagementPortal.Api.Model
{
    public class EmployeeAttendanceRepository : IEmployeeAttendanceRepository
    {
        private AppDbContext _appDbContext { get; set; }
        public EmployeeAttendanceRepository(AppDbContext appDbContext) 
        { 
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendanceFromBiometric()
        {
            var result = (from ea in _appDbContext.EmployeesAttendances
                          join emp in _appDbContext.Employees on ea.EmployeeID equals emp.ID
                          join attendance in (from log in _appDbContext.AttendanceLogs
                                              group log by new { log.EmployeeID, log.Type, log.Date } into logs

                                              select new { EmployeeID = logs.Key.EmployeeID, Type = logs.Key.Type, MaxDate = logs.Max(x => x.Date) })
                         on new { ea.EmployeeID } equals new { attendance.EmployeeID }

                          //join loc in _appDbContext.ValidWiFis on Convert.ToInt32(attendance.Source) equals loc.ID
                          where attendance.MaxDate == ea.Date && attendance.Type == "In"
                          select new EmployeeAttendance
                          {
                              ID = ea.ID,
                              CheckIn = ea.CheckIn,
                              CheckOut = ea.CheckOut,
                              Date = ea.Date,
                              EmployeeID = ea.EmployeeID,
                              Employee = emp,
                              //Location = loc.Location
                          })

                          .OrderByDescending(x => x.Date).ToList();
            await Task.CompletedTask;
            return result;

            
        }

        public async Task<IEnumerable<EmployeeAttendance>> GetEmployeeAttendance()
        {
            var result =  (from ea in _appDbContext.EmployeesAttendances
                           join emp in _appDbContext.Employees on ea.EmployeeID equals emp.ID
                         join attendance in ( from log in _appDbContext.AttendanceLogs
                                              group log by new { log.EmployeeID, log.Source , log.Type , log.Date } into logs

                        select new { EmployeeID = logs.Key.EmployeeID ,Source = logs.Key.Source , Type = logs.Key.Type  , MaxDate = logs.Max(x => x.Date) } ) 
                        on new { ea.EmployeeID } equals new { attendance.EmployeeID }

                          join loc in _appDbContext.ValidWiFis on Convert.ToInt32(attendance.Source) equals loc.ID
                         where attendance.MaxDate == ea.Date && attendance.Type == "In"
                          select new EmployeeAttendance
                          {
                              ID = ea.ID,
                              CheckIn = ea.CheckIn,
                              CheckOut = ea.CheckOut,
                              Date = ea.Date,
                              EmployeeID = ea.EmployeeID,
                              Employee = emp,
                              Location = loc.Location
                          })
                          
                          .OrderByDescending(x => x.Date).ToList();
            await Task.CompletedTask;
            return result;

            
        }

        public async Task<EmployeeAttendance> CreateEmployeeAttendance(EmployeeAttendance employeeAttendance)
        {
            try
            {
                var result = await _appDbContext.EmployeesAttendances.AddAsync(employeeAttendance);
                await _appDbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message: {0}", e.Message);
                Console.WriteLine("Stack Trace: {0}", e.StackTrace);
                throw;
            }
            
        }
        
        public async Task<EmployeeAttendance?> GetEmployeeAttendanceByEmployeeId(int employeeId) 
        {
            try
            {
                var result = await _appDbContext.EmployeesAttendances
                                .FirstOrDefaultAsync(x => x.EmployeeID == employeeId);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message: {0}", e.Message);
                Console.WriteLine("Stack Trace: {0}", e.StackTrace);
                throw;
            }
        }
        public async Task<EmployeeAttendance?> GetEmployeeAttendanceByEmployeeIdForToday(int employeeId)
        {
            try
            {
                var result = await _appDbContext.EmployeesAttendances
                                .Where(y => y.Date == DateTime.Today)
                                .FirstOrDefaultAsync(x => x.EmployeeID == employeeId);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message: {0}", e.Message);
                Console.WriteLine("Stack Trace: {0}", e.StackTrace);
                throw;
            }
        }

    }
}
