using Microsoft.AspNetCore.Mvc;

namespace UniversityManagementSystem.API.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
