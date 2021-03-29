using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Web_Development.Middleware.Auth
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (new Utils.Auth(context.Response).User() == null)
            {
                context.Response.Redirect("/");
                return;
            }

            await _next.Invoke(context);
        }
    }
}