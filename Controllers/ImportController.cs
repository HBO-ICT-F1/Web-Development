using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Development.Middleware.Auth;
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
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Import()
        {
            // Get uploaded files
            var files = Request.Form.Files;
            if (files.Count != 1) return RedirectToAction("Index", "Account");

            // Read file contents from stream
            var contents = files[0].OpenReadStream().ReadString();
            var records = _database.Records.ToList();

            // Create missing records and add to the database
            var created = Importer.CreateMissing(contents, ref records);
            if (created.Count != 0)
            {
                _database.AddRange(created);
                _database.SaveChanges();
            }

            // Import products from csv and add to database
            var products = Importer.GetProducts(user, contents, records);
            _database.AddRange(products);
            _database.SaveChanges();

            // Redirect user to profile page
            Console.WriteLine("Imported {0} products and created {1} records", products.Count, created.Count);
            return RedirectToAction("Index", "Account");
        }
    }
}