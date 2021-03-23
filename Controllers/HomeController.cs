using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Development.Controllers
{
    public class HomeController : Controller
    {
        private readonly Database _database;

        public HomeController(Database database)
        {
            _database = database;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            ViewBag.products = _database.Products
                .Include(p => p.Record)
                .OrderByDescending(p => p.Id)
                .Take(5);
            return View("Index");
        }
    }
}