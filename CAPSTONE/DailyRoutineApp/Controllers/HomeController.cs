using Microsoft.AspNetCore.Mvc;

namespace DailyRoutineApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => RedirectToAction("Index", "Routine");
    }
}