using System;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.Logging;
using ProjectManagementWebApp.Models;

using Serilog;

namespace ProjectManagementWebApp
{
    public class Program
    {
        public static int Main(string[] args)
        {
            SeriLogConfiguration.CreateLoggerConfiguration();

            try
            {
                var host = CreateHostBuilder(args).Build();

                CreateDbIfNotExists(host);

                host.Run();

                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");

                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ProjectManagementContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

                    DbInitializer.Initialize(context, userManager, roleManager);
                }
                catch (Exception)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError("An error occurred creating the database.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
