using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Development.Middleware.Auth;
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
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Index()
        {
            ViewBag.records = _database.Records;
            return View("Index", auth);
        }
    }
}