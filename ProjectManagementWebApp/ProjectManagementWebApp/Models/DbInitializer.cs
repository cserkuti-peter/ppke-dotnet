using System;
using System.Linq;

using Microsoft.AspNetCore.Identity;

using ProjectManagementWebApp.Data;

namespace ProjectManagementWebApp.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ProjectManagementContext context,
            UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (context.Project.Any())
            {
                return;
            }

            var projects = new Project[]
            {
                new Project  { Name = "Project #1", Description = "Description #1"},
                new Project  { Name = "Project #2", Description = "Description #2"},
                new Project  { Name = "Project #3", Description = "Description #3"},
                new Project  { Name = "Project #4", Description = "Description #4"},
            };

            context.Project.AddRange(projects);
            context.SaveChanges();

            InitializeRolesAsync(roleManager).GetAwaiter().GetResult();
            InitializeAdminUser(userManager).GetAwaiter().GetResult();
        }

        private static async System.Threading.Tasks.Task InitializeRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            var roles = Enum.GetNames(typeof(Role));

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(new ApplicationRole { Name = role });
            }
        }

        private static async System.Threading.Tasks.Task InitializeAdminUser(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser
            {
                Name = "Admin User",
                UserName = "admin@ProjectManagementWebApp.com",
                Email = "admin@ProjectManagementWebApp.com",
                Role = Role.Admin
            };

            var createResult = await userManager.CreateAsync(user, "4Dm1n_634h");

            Console.WriteLine(createResult.Succeeded);

            await userManager.AddToRoleAsync(user, Role.Admin.ToString());
        }
    }
}
