using Microsoft.AspNetCore.Mvc;

namespace Web_Development.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}