using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Web_Development
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure host builder
            var builder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(hostBuilder => hostBuilder.UseStartup<Program>());

            // Build ASP.NET host
            var host = builder.Build();

            // Make sure the database exists before running ASP.NET server
            if (!Database.Create(host)) return;
            host.Run();
        }
        
        public static void ConfigureServices(IServiceCollection services)
        {
            // Add database context to project
            services.AddDbContext<Database>();

            // Add razor pages to project
            services.AddRazorPages();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Set ASP.NET preferences
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            // Add endpoints
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapRazorPages());
        }
    }
}