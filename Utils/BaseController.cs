using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web_Development.Utils
{
    public class BaseController : Controller
    {
        public Auth _auth;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _auth = new Auth(httpContextAccessor.HttpContext?.Response);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.auth = _auth;
            ViewBag.user = _auth.User();
            base.OnActionExecuting(filterContext);
        }
    }
}