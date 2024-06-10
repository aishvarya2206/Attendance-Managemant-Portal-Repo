using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagementPortal.Model
{
    public class AttendanceLog
    {
        public int ID { get; set; }
        public DateTime Date { get; set; } = DateTime.Today;
        public TimeSpan Time { get; set; } = DateTime.Now.TimeOfDay;
        public string Type { get; set; } = string.Empty;
        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }
        public string Source { get; set; } = string.Empty;

    }

}
