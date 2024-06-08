using Microsoft.AspNetCore.Mvc;

namespace MiniProject_eLearning_ASPNET_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
