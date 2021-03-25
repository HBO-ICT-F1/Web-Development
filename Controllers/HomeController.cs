using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Development.Utils;

namespace Web_Development.Controllers
{
    public class HomeController : BaseController
    {
        private readonly Database _database;

        public HomeController(Database database, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
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