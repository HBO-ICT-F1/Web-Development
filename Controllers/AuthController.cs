using Microsoft.AspNetCore.Mvc;

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
    }
}