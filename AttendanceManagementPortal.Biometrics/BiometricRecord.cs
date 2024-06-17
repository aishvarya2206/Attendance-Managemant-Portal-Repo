using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceManagementPortal.Biometrics
{
    public class BiometricRecord
    {
        public int Employee_ID { get; set; }
        public string Employee_FullName { get; set; }
        public string Employee_UserName { get; set; }
        public int Department_ID { get; set; }
        public string Department { get; set; }
        public TimeSpan DateTime_IN { get; set; }
        public TimeSpan DateTime_OUT { get; set; }
    }
}
