using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AttendanceManagementPortal.Model
{
    public class Department
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
