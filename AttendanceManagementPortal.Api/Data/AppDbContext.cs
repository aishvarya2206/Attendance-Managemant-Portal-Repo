using AttendanceManagementPortal.Model;
using AttendanceManagementPortal.Web.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace AttendanceManagementPortal.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<EmployeeAttendance> EmployeesAttendances { get; set; }
        public DbSet<ValidWiFi> ValidWiFis { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<AttendanceLog> AttendanceLogs { get; set; }

        // Seed data save in db using onmodelcreating component method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>().HasData(
            new Department
            {
                ID = 1,
                Name = "TestDepartment1"
            },
            new Department
            {
                ID = 2,
                Name = "TestDepartment2"
            }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    ID = 1,
                    FullName = "TestEmployee1",
                    Code = "DEP123",
                    UserName = "TestName1",
                    DeviceIPAddress = "fe80::428d:cba:b00a:7832%20",
                    DepartmentId = 1
                },
                new Employee
                {
                    ID = 2,
                    FullName = "TestEmployee2",
                    Code = "DEP124",
                    UserName = "TestName2",
                    DeviceIPAddress = "fe80::428d:cba:b00a:7832%21",
                    DepartmentId = 2
                }
            );
            modelBuilder.Entity<ValidWiFi>().HasData(
            new ValidWiFi
            {
                ID = 1,
                SSID = "Raj-5G",
                Location = "Gurgaon"
            }
           /* new ValidWiFi
            {
                ID = 2,
                SSID = "JacobsGuest",
                Location = "Gurgaon"
            }*/
            );

           
        }
    }
}
