using System;
using Microsoft.AspNetCore.Mvc;
using Web_Development.Models;
using static BCrypt.Net.BCrypt;

namespace Web_Development.Controllers
{
    public class AuthController : Controller
    {
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
            using (var database = new Database())
            {
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
                database.Add(user);
                database.SaveChanges();
            }
            return Redirect("/login");
        }
    }
}