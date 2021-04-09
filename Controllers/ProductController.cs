using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Development.Middleware.Auth;
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

        [HttpGet("/product/create")]
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Add()
        {
            return View("Create");
        }

        [HttpPost("/product/create")]
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Create()
        {
            // return RedirectToAction("Index", "Account");
            return RedirectToAction("Add");
        }

        [HttpGet("/product/update/{productId}")]
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Edit(int productId)
        {
            var product = ViewBag.product =  _database.Products
                .Include(product => product.Record)
                .FirstOrDefault(product => product.Id == productId);
            if (product == null) return RedirectToAction("Index", "Account");
            return View("Update");
        }

        [HttpPost("/product/update/{productId}")]
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Update(int productId)
        {
            var product = _database.Products.FirstOrDefault(product => product.Id == productId);
            if (product == null) return RedirectToAction("Edit");

            // return RedirectToAction("Index", "Account");
            return RedirectToAction("Add");
        }
    }
}