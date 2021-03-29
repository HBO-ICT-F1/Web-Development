using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web_Development.Models;

namespace Web_Development.Utils
{
    public class BaseController : Controller
    {
        protected readonly Auth auth;
        protected readonly User user;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            auth = new Auth(httpContextAccessor.HttpContext?.Response);
            user = auth.User();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.auth = auth;
            ViewBag.user = user;
            base.OnActionExecuting(filterContext);
        }
    }
}