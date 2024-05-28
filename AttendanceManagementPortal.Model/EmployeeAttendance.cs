using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagementPortal.Model
{
    public class EmployeeAttendance
    {
        public int ID { get; set; }
        public string CheckIn { get; set; } = string.Empty;
        public string CheckOut { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public int EmployeeID { get; set; }
        public Employee? Employee { get; set; }

    }
}
