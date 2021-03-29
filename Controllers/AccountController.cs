using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Development.Middleware.Auth;
using Web_Development.Utils;
using static BCrypt.Net.BCrypt;

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

        [HttpGet("/account")]
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult Index()
        {
            ViewBag.purchasableProducts = _database.Products
                .Include(product => product.Record)
                .Where(product => product.ForSale)
                .OrderByDescending(product => product.Id);

            ViewBag.soldProducts = _database.Products
                .Include(product => product.Record)
                .Where(product => !product.ForSale)
                .OrderByDescending(product => product.Id);
            return View("Index");
        }

        [HttpPost("/account/password")]
        [MiddlewareFilter(typeof(AuthMiddlewareConfig))]
        public IActionResult PasswordChange()
        {
            var oldPassword = Request.Form["Password"];
            var newPassword = Request.Form["NewPassword"];
            if (user == null || !EnhancedVerify(oldPassword, user.Password))
            {
                TempData["error_password"] = "Het ingevulde wachtwoord is onjuist";
                return RedirectToAction("Index");
            }

            user.Password = EnhancedHashPassword(newPassword);
            _database.Users.Update(user);
            auth.Login(user);
            _database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}