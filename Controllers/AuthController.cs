using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web_Development.Models;
using Web_Development.Utils;
using static BCrypt.Net.BCrypt;

namespace Web_Development.Controllers
{
    public class AuthController : BaseController
    {
        private readonly Database _database;

        public AuthController(Database database, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
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

            var foundUser = _database.Users.FirstOrDefault(user => user.Email == email);
            if (foundUser != null && EnhancedVerify(password, foundUser.Password))
            {
                _auth.Login(foundUser);
                return RedirectToAction("Index", "Home");
            }
            TempData["error"] = "De ingevulde gegevens zijn niet bekend";
            return RedirectToAction("Login");
        }
        
        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            _auth.Logout();
            return RedirectToAction("Index", "Home");
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