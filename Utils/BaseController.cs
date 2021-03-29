using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web_Development.Models;

namespace Web_Development.Utils
{
    public class BaseController : Controller
    {
        public Auth _auth;
        public User _user;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _auth = new Auth(httpContextAccessor.HttpContext?.Response);
            _user = _auth.User();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.auth = _auth;
            ViewBag.user = _user;
            base.OnActionExecuting(filterContext);
        }
    }
}