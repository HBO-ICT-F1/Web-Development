using Microsoft.AspNetCore.Mvc;

namespace Web_Development.Controllers
{
    public class CatalogueController : Controller
    {
        [HttpGet("/catalogue")]
        public IActionResult Index()
        {
            return View();
        }
    }
}