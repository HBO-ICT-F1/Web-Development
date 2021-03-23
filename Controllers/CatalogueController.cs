using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Development.Controllers
{
    public class CatalogueController : Controller
    {
        private readonly Database _database;

        public CatalogueController(Database database)
        {
            _database = database;
        }

        [HttpGet("/catalogue")]
        public IActionResult Index()
        {
            ViewBag.products = _database.Products.Include(p => p.Record);
            return View();
        }
    }
}