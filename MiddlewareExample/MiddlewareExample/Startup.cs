using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MiddlewareExample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                }
                catch (Exception ex)
                {
                    await context.Response.WriteAsync($"Something has went wrong: {ex}");
                }
            });

            app.UseRequestCulture();

            app.Run(async (context) =>
            {
                throw new Exception("Test exception");
                await context.Response.WriteAsync(
                    $"Hello world!");
            });
        }

        public static void HandleTestSites(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("This is a test site");
            });
        }
    }
}
