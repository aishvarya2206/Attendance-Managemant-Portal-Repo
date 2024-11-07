using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagementPortal.Model
{
    public class Manager
    {
        public int ID { get; set; }
        public int ManagerID { get; set; }
        public int EmployeeID { get; set; }
        public required Employee Employee { get; set; }
    }
}
