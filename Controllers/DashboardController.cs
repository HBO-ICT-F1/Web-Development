using Microsoft.AspNetCore.Mvc;

namespace Web_Development.Controllers
{
    public class DashboardController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}