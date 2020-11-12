using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagementWebApp.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp
{
    public static class DBSeeding
    {
        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            using (var context = new ProjectManagementContext(
                serviceProvider.GetRequiredService<DbContextOptions<ProjectManagementContext>>()))
            {
                if (context.Project.Any())
                {
                    return;
                }

                context.Project.Add(new Models.Project
                {
                    Description = "seeded from custom",
                    Name = "customseed",
                    Secret = "customsecret"
                });

                context.SaveChanges();

            }
        }

    }
}
