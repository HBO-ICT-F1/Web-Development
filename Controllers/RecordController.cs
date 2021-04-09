using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Development.Middleware.Auth;
using Web_Development.Models;
using Web_Development.Utils;

namespace Web_Development.Controllers
{
    public class RecordController : BaseController
    {
        private readonly Database _database;

        public RecordController(Database database, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _database = database;
        }

        [HttpGet("/record/{recordId}")]
        public IActionResult Index(int recordId)
        {
            ViewBag.record = _database.Records.FirstOrDefault(record => record.Id == recordId);
            ViewBag.products = _database.Products
                .Include(product => product.User)
                .Where(product => product.ForSale && product.RecordId == recordId && product.Sale == null)
                .OrderBy(product => product.Price );
            return View("Index");
        }

        [HttpGet("/record/{recordId}/product/{productId}/purchase")]
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Purchase(int recordId, int productId)
        {
            //TODO: Add paypal sandbox implementation
            var product = _database.Products.FirstOrDefault(product => product.Id == productId);
            if (product == null || product.UserId == user.Id)
            {
                return RedirectToAction("Index", new {RecordId = recordId});
            }
            _database.Add(new Sale
            {
                ProductId = productId,
                UserId = user.Id,
                CreatedAt = DateTime.Now
            });
            _database.SaveChanges();
            return RedirectToAction("Index", new {RecordId = recordId});
        }
    }
}