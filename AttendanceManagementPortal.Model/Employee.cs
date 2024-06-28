using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AttendanceManagementPortal.Model
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string DeviceIPAddress { get; set; } = string.Empty;
        [Required(ErrorMessage ="Select DepartmentId")]
        [DefaultValue(1)]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }


    }
}
 