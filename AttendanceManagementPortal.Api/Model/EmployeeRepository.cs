using AttendanceManagementPortal.Api.Data;
using AttendanceManagementPortal.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AttendanceManagementPortal.Api.Model
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private AppDbContext _appDbContext { get; set; }
        private EmployeeAttendance _empAttd;
        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _empAttd = new EmployeeAttendance();
        }
        
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _appDbContext.Employees
                .Include(x => x.Department)
                .ToListAsync();
        }
        public async Task<Employee?> GetEmployee(int employeeId)
        {
            return await _appDbContext.Employees
                .Include(x => x.Department)
                .FirstOrDefaultAsync(e => e.ID == employeeId);
        }

        public async Task<EmployeeAttendance?> MarkAttendances([FromForm] RequestData requestData)
        {
            try
            {
                    var employeeAttendance = new EmployeeAttendance();
                    var checkwifi = await _appDbContext.ValidWiFis.FirstOrDefaultAsync(w => w.SSID == requestData.WifiSsid);
                    if (checkwifi != null)
                    {
                        Employee? employee = await _appDbContext.Employees.FirstOrDefaultAsync(e => e.DeviceIPAddress == requestData.IpAddress);
                        if (employee != null)
                        {
                            employeeAttendance.Employee = employee;
                            employeeAttendance.CheckIn = DateTime.Now.ToLongTimeString();
                            employeeAttendance.CheckOut = "18:00";
                            employeeAttendance.Date = DateTime.Today;

                            _appDbContext.EmployeesAttendances.Add(employeeAttendance);
                            await _appDbContext.SaveChangesAsync();

                        }
                        return employeeAttendance;
                    }
                    return employeeAttendance;
                
                
            }
            catch (Exception e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message: {0}", e.Message);
                Console.WriteLine("Stack Trace: {0}", e.StackTrace);
                throw;
            }
        }

        public async Task<Employee> CreateEmployee(Employee employee)
        {
            try
            {
                var result = await _appDbContext.Employees.AddAsync(employee);
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
    }
}
