using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Development.Utils;

namespace Web_Development.Controllers
{
    public class ImportController : BaseController
    {
        private readonly Database _database;

        public ImportController(Database database, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _database = database;
        }

        [HttpPost("/account/import")]
        public IActionResult Import()
        {
            return RedirectToAction("Index", "Account");
        }
    }
}