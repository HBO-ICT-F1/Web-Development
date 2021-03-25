using Microsoft.AspNetCore.Builder;

namespace Web_Development.Middleware.Auth
{
    public class AuthMiddlewareConfig
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<AuthMiddleware>();
        }
    }
}