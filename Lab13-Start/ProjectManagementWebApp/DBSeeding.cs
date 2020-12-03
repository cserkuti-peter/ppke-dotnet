using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.Models;
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
            using (var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>())
            using (var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>())
            {
                context.Database.EnsureCreated();

                if (context.Project.Any())
                {
                    return;
                }
                InitializeRolesAsync(roleManager).GetAwaiter().GetResult();
                InitializeAdminUserAsync(userManager).GetAwaiter().GetResult();

                var proj = new Models.Project
                {
                    Description = "seeded from custom",
                    Name = "customseed"
                };
                context.Project.Add(proj);
                context.Tasks.Add(new TaskModel { Description = "first task", Name = "task1", Project = proj});

                context.SaveChanges();

          
            }
        }

        public static async Task InitializeRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            var roles = Enum.GetNames(typeof(RoleValues));
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = role });
            }
        }

        public static async Task<ApplicationUser> InitializeAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser
            {
                Name = "Admin User",
                UserName = "admin@ProjectManagemengtWebApp.com",
                Email = "admin@ProjectManagemengtWebApp.com",
                Role = RoleValues.Admin,
                EmailConfirmed = true
            };
            var createResult = await userManager.CreateAsync(user, "asdasd9");
            Console.WriteLine($"Creating admin user succeeded? {createResult.Succeeded}");

            await userManager.AddToRoleAsync(user, RoleValues.Admin.ToString());
            return user;
        }
    }
}
