using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Development.Middleware.Auth;
using Web_Development.Utils;

namespace Web_Development.Controllers
{
    public class AccountController : BaseController
    {
        private readonly Database _database;

        public AccountController(Database database, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _database = database;
        }

        [HttpGet("/account"), MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}