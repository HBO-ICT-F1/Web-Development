using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Development.Utils;

namespace Web_Development.Controllers
{
    public class ProductController : BaseController
    {
        private readonly Database _database;

        public ProductController(Database database, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
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