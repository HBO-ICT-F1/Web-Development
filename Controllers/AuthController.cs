﻿using System;
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
        
        [BindProperty] 
        public NewUser CreateNewUser { get; set; }

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
            var email = Request.Form["Email"].ToString();
            var password = Request.Form["Password"];


            var foundUser = _database.Users.FirstOrDefault(user => user.Email == email.ToLower());
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
                var validationResults = new List<ValidationResult>();
                var context = new ValidationContext(CreateNewUser);
                var isValid = Validator.TryValidateObject(CreateNewUser, context, validationResults, true);

                foreach (var x in validationResults.ToArray())
                    TempData[$"error_for_{x.MemberNames.First()}"] = x.ErrorMessage;

                //Todo: Keep correct values
                return View("Register");
            }

            var foundUser = _database.Users.FirstOrDefault(user => user.Email == CreateNewUser.Email.ToLower());
            if (foundUser != null)
            {
                TempData["error_for_email"] = "Email bestaat al.";
                return View("Register");
            }

            var hash = EnhancedHashPassword(CreateNewUser.Password);

            var user = new User
            {
                Name = CreateNewUser.Name,
                Email = CreateNewUser.Email.ToLower(),
                Password = hash,
                Address = CreateNewUser.Address,
                PostalCode = CreateNewUser.PostCode,
                Country = CreateNewUser.Country,
                Role = 0,
                CreatedAt = DateTime.Now
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

            [Required(ErrorMessage = "Je moet een wachtwoord opgeven")]
            [MinLength(6, ErrorMessage = "Je wachtwoord moet minimaal 6 characters lang zijn")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Je moet een adress opgeven!")]
            [MinLength(4, ErrorMessage = "Je moet een adress opgeven!")]
            public string Address { get; set; }

            [Required(ErrorMessage = "Je moet een postcode opgeven!")]
            [MinLength(4, ErrorMessage = "Dit is geen geldige postcode")]
            public string PostCode { get; set; }

            [Required(ErrorMessage = "Je moet een land opgeven!")]
            [MinLength(3, ErrorMessage = "Dit is geen geldig land!")]
            public string Country { get; set; }
        }
    }
}