using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Api.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
