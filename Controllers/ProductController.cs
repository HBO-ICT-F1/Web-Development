using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Development.Models;
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

        [HttpGet("/record/{RecordId}")]
        public IActionResult Index(int recordId)
        {
            ViewBag.record = _database.Records.FirstOrDefault(record => record.Id == recordId);
            ViewBag.products = _database.Products
                .Include(product => product.User)
                .Where(product => product.ForSale && product.RecordId == recordId)
                .OrderBy(product => product.Price );
            return View("Index");
        }
    }
}