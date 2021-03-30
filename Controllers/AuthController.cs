using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [BindProperty] public NewUser CreateNewUser { get; set; }
        

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
                auth.Login(foundUser);
                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = "De ingevulde gegevens zijn niet bekend";
            return RedirectToAction("Login");
        }

        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            auth.Logout();
            return RedirectToAction("Index", "Home");
        }
        

        [HttpPost("/register")]
        public IActionResult CreateUser()
        {

            if (!ModelState.IsValid)
            {

        
                List<ValidationResult> validationResults = new List<ValidationResult>();
                var context = new ValidationContext(CreateNewUser);
                bool isValid = Validator.TryValidateObject(CreateNewUser, context, validationResults, true);

                foreach (ValidationResult x in validationResults.ToArray())
                {
                    TempData[$"error_for_{x.MemberNames.First()}"] = x.ErrorMessage;
                    
                    //Todo: Keep correct values
                }
                return View("Register");

            }

            
            var hash = EnhancedHashPassword(CreateNewUser.Password);
            
            var user = new User
            {
                Name = CreateNewUser.Name,
                Email = CreateNewUser.Email,
                Password = hash,
                Address = CreateNewUser.Address,
                PostalCode = CreateNewUser.PostCode,
                Country = CreateNewUser.Country,
                Role = 0,
                CreatedAt = DateTime.Today
            };
            _database.Add(user);
            _database.SaveChanges();

            //TODO: Find better solution
            return Redirect("/login");
        }

        public class NewUser
        {
            [Required(ErrorMessage = "Je moet een naam opgeven")]
            public string Name { get; set; }
            
            [EmailAddress(ErrorMessage = "Je moet een geldig email adres opgeven!")]
            public string Email { get; set; }
            [Required(ErrorMessage = "Je moet een wachtwoord opgeven"), MinLength(6, ErrorMessage = "Je wachtwoord moet minimaal 6 characters lang zijn")]
            public string Password { get; set; }
            [Required(ErrorMessage = "Je moet een adress opgeven!"), MinLength(4, ErrorMessage = "Je moet een adress opgeven!")]
            public string Address { get; set; }
            [Required(ErrorMessage = "Je moet een postcode opgeven!"), MinLength(4, ErrorMessage = "Dit is geen geldige postcode")]
            public string PostCode { get; set; }
            [Required(ErrorMessage = "Je moet een land opgeven!"), MinLength(3, ErrorMessage = "Dit is geen geldig land!")]
            public string Country { get; set; }
            
        }
        
    }
}