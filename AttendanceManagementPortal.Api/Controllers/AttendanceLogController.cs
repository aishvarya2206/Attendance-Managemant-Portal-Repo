using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Api.Controllers
{
    public class AttendanceLogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
