using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagementPortal.Model
{
    public class ValidWiFi
    {
        public int ID { get; set; }
        public string SSID { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
