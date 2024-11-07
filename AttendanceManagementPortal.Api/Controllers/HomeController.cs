using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagementPortal.Api.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("Index Method Called!!!");
            return View();
        }

    }
}
