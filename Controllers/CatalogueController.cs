using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Development.Utils;

namespace Web_Development.Controllers
{
    public class CatalogueController : BaseController
    {
        private readonly Database _database;

        public CatalogueController(Database database, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _database = database;
        }

        [HttpGet("/catalogue")]
        public IActionResult Index()
        {
            ViewBag.products = _database.Products
                .Include(p => p.Record);
            return View("Index", _auth);
        }
    }
}