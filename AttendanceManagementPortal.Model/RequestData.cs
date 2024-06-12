using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagementPortal.Model
{
    public class RequestData
    {
        public string? WifiSsid { get; set; }
        public string IpAddress { get; set; } = string.Empty;
    }
}
