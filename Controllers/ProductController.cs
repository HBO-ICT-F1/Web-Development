using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web_Development.Controllers
{
    public class ProductController : Controller
    {
        private readonly Database _database;

        public ProductController(Database database)
        {
            _database = database;
        }
        
        [HttpGet("/product/{ProductId}")]
        public IActionResult Index(int ProductId)
        {
            ViewBag.product = _database.Products
                .Include(p => p.Record)
                .Include(p => p.User)
                .FirstOrDefault(b => b.Id == ProductId);
            return View("Index");
        }
    }
}