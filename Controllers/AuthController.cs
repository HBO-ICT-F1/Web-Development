using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Web_Development.Models;
using static BCrypt.Net.BCrypt;

namespace Web_Development.Controllers
{
    public class AuthController : Controller
    {
        private readonly Database _database;

        public AuthController(Database database)
        {
            _database = database;
        }
        
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost("/login")]
        public IActionResult Auth()
        {
            var email = Request.Form["Email"];
            var password = Request.Form["Password"];

            var user = _database.Users.FirstOrDefault(b => b.Email == email);
            if (user != null && EnhancedVerify(password, user.Password))
            {
                return RedirectToAction("Index", "Home");
            }
            TempData["error"] = "De ingevulde gegevens zijn niet bekend";
            return RedirectToAction("Login");
        }

        [HttpPost("/register")]
        public IActionResult CreateUser()
        {
            var name = Request.Form["Name"];
            var email = Request.Form["Email"];
            var password = Request.Form["Password"];
            var address = Request.Form["Address"];
            var postCode = Request.Form["PostalCode"];
            var country = Request.Form["Country"];
            var hash = EnhancedHashPassword(password);
            //TODO: Add validator
            var user = new User
            {
                Name = name,
                Email = email,
                Password = hash,
                Address = address,
                PostalCode = postCode,
                Country = country,
                Role = 0,
                CreatedAt = DateTime.Today
            };
            _database.Add(user);
            _database.SaveChanges();
            
            //TODO: Find better solution
            return Redirect("/login");
        }
    }
}